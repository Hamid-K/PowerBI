# Transformation.Text specific semantics

import calendar
import collections
import math
import decimal
import datetime
import itertools

class ImmutableDictionary(collections.Mapping):
    __slots__ = ('_dict', '_hash_code')
    
    def __init__(self, input_dict):
        self._dict = dict(input_dict)
        self._hash_code = None
        
    def __iter__(self):
        return iter(self._dict)
    
    def __len__(self):
        return len(self._dict)
    
    def __getitem__(self, item):
        return self._dict[item]
    
    def __hash__(self):
        if self._hash_code is None:
            self._hash_code = functools.reduce(lambda acc, pair: acc ^ hash(pair), self._dict.items(), 0)
        return self._hash_code
    
    def __eq__(self, other):
        if not isinstance(other, ImmutableDictionary):
            return False
        if hash(self) != hash(other):
            return False
        return self._dict == other._dict
    
    def __ne__(self, other):
        return not (self == other)
    
    def __str__(self):
        return ', '.join(['{0} : {1}'.format(str(k), str(v)) for k, v in self._dict.items()])
    
    def __repr__(self):
        return 'ImmutableDictionary(' + str(self) + ')'
    

_camel_case_to_snake_case_regex = regex.compile('(\p{Ll}?)(\p{Lu})(\p{Ll})')


def _camel_case_to_snake_case_repl(m):
    if len(m.group(1)) > 0:
        return m.group(1) + '_' + m.group(2).lower() + m.group(3)
    else:
        return m.group(2).lower() + m.group(3)


def _camel_case_to_snake_case(s):
    """
    >>> _camel_case_to_snake_case('HelloWorld')
    'hello_world'
    >>> _camel_case_to_snake_case('Hello')
    'hello'
    >>> _camel_case_to_snake_case('HelloWorldAgain')
    'hello_world_again'
    >>> _camel_case_to_snake_case('helloWorld')
    'hello_world'
    >>> _camel_case_to_snake_case('helloWorldAgain')
    'hello_world_again'
    >>> _camel_case_to_snake_case('helloworldagain')
    'helloworldagain'
    >>> _camel_case_to_snake_case('_HelloWorld')
    '_hello_world'
    >>> _camel_case_to_snake_case('_Hello_World')
    '_hello_world'
    """
    return _camel_case_to_snake_case_regex.sub(_camel_case_to_snake_case_repl, s)
    

class DateTimeFormatPart(object):
    __slots__ = ('matched_part',)
    cache = {}

    def __init__(self, matched_part):
        self.matched_part = matched_part

    @classmethod
    def clear_cache(cls):
        cls.cache = {}

    def __eq__(self, other):
        return self is other

    def __ne__(self, other):
        return self is not other

    def populate_constructor_dict(self, constructor_dict, v):
        if self.matched_part is not None and constructor_dict is not None and v is not None:
            constructor_dict[self.matched_part] = v

    def is_numeric(self):
        return False

    def is_numeric_at_end(self):
        return False


class ConstantDateTimeFormatPart(DateTimeFormatPart):
    __slots__ = ('constant_string',)

    def __init__(self):
        super().__init__(None)

    @classmethod
    def create(cls, constant_string):
        r = cls.cache.get(constant_string, None)
        if r is None:
            r = ConstantDateTimeFormatPart()
            r.constant_string = constant_string
            cls.cache[constant_string] = r
        return r

    def format(self, v):
        return self.constant_string

    def parse(self, region, constructor_dict=None, diff_from_min_length=None):
        region_length = len(region)
        constant_length = len(self.constant_string)
        if region_length < constant_length:
            return False, region
        if region.content[region.start:region.start + constant_length] != self.constant_string:
            return False, region
        self.populate_constructor_dict(constructor_dict, self.constant_string)
        return True, region.slice(region.start + constant_length, region.end)

    def __hash__(self):
        return hash(self.constant_string) ^ hash(len(self.constant_string)) ^ hash(self.matched_part)


class NumericDateTimeFormatPart(DateTimeFormatPart):
    __slots__ = ('parse_func', 'output_func', 'min_length', 'max_length', 'mod_value', 'parse_regex', 'min_value', 'max_value')
    _cache = {}

    def __init__(self, matched_part):
        super().__init__(matched_part)
        if matched_part == 'millisecond':
            self.parse_func = lambda x: int(x * (10 ** (3 - self.max_length)))
            self.output_func = lambda x: int(x / (10 ** (3 - self.max_length)))
        else:
            self.parse_func = lambda x: x
            self.output_func = lambda x: x

    @classmethod
    def create(cls, min_length, max_length, matched_part, min_value=None, max_value=None, parse_func=None, allow_leading_zeros=False):
        r = cls._cache.get((min_length, max_length, matched_part, min_value, max_value), None)
        if r is None:
            r = NumericDateTimeFormatPart(matched_part)
            r.min_value = min_value
            r.max_value = max_value
            r.min_length = min_length
            r.max_length = max_length
            if parse_func is not None:
                r.parse_func = parse_func
            r.mod_value = int('1' + '0' * max_length)
            if max_length > min_length and not allow_leading_zeros:
                r.parse_regex = regex.compile(
                    '([1-9][0-9]{0,' + str(max_length - min_length - 1) + '})?[0-9]{' + str(min_length) + '}')
            elif min_length == max_length:
                r.parse_regex = regex.compile('[0-9]{' + str(min_length) + '}')
            else:
                r.parse_regex = regex.compile('[0-9]{' + str(min_length) + ',' + str(max_length) + '}')
            cls._cache[(min_length, max_length, matched_part)] = r
        return r

    def format(self, v):
        if self.matched_part is None:
            return None
        v_matched_part = getattr(v, self.matched_part)
        if v_matched_part is None:
            return None
        return '{0:0{width}d}'.format(self.output_func(v_matched_part) % self.mod_value, width=self.min_length)

    def parse(self, region, constructor_dict, diff_from_min_length=None):
        if self.min_length != self.max_length and diff_from_min_length is not None:
            m = self.parse_regex.match(region.content, pos=region.start,
                                       endpos=region.start+self.min_length+diff_from_min_length)
        else:
            m = self.parse_regex.match(region.content, pos=region.start)

        if m is None:
            return False, region
        match_string = region.content[m.start():m.end()]
        match_length = len(match_string)
        match_value = int(match_string)
        if (self.min_value is not None and match_value < self.min_value) or\
            (self.max_value is not None and match_value > self.max_value):
            return False, region
        self.populate_constructor_dict(constructor_dict, self.parse_func(match_value))
        return True, region.slice(region.start + match_length, region.end)

    def is_numeric(self):
        return True

    def __hash__(self):
        return hash(self.min_length) ^ hash(self.max_length) % hash(self.matched_part)


class TimeZoneOffsetFormatPart(DateTimeFormatPart):
    __slots__ = ('min_length', 'max_length', 'separator', 'zero_is_z', 'allow_numeric_zero', 'parse_regex')
    _cache = {}

    def __init__(self, separator, matched_part, zero_is_z, allow_numeric_zero):
        super().__init__(matched_part)
        self.separator = separator
        self.zero_is_z = zero_is_z
        self.allow_numeric_zero = allow_numeric_zero

    @classmethod
    def create(cls, separator, matched_part, zero_is_z, allow_numeric_zero):
        key = (separator, matched_part, zero_is_z, allow_numeric_zero)
        r = cls._cache.get(key, None)
        if r is None:
            r = TimeZoneOffsetFormatPart(separator, matched_part, zero_is_z, allow_numeric_zero)
            r.max_length = len(separator) + 5
            regex_str = '(?P<sign>[-+])' + '(?P<hour>[01][0-9]|2[0-4])' + regex.escape(separator) + '(?P<minute>00|30|45)'
            if zero_is_z:
                r.min_length = 1
                regex_str = '(?P<Z>Z)|' + regex_str
            else:
                r.min_length = r.max_length
            r.parse_regex = regex.compile(regex_str)
            cls._cache[key] = r
        return r

    def format(self, v):
        if self.matched_part is None:
            return None
        v_matched_part = getattr(v, self.matched_part)
        if v_matched_part is None:
            return None
        if v_matched_part == 0 and self.zero_is_z:
            return 'Z'
        else:
            if v_matched_part < 0:
                sign = '-'
                v_matched_part = -v_matched_part
            else:
                sign = '+'
            return sign + '{0:02d}'.format(v_matched_part / 60) + self.separator + '{0:02d}'.format(v_matched_part % 60)

    def parse(self, region, constructor_dict, diff_from_min_length=None):
        m = self.parse_regex.match(region.content, pos=region.start)

        if m is None:
            return False, region

        if m.group('Z') is not None:
            if self.zero_is_z:
                match_value = 0
            else:
                return False, region
        else:
            match_value = 60 * int(m.group('hour')) + int(m.group('minute'))
            if m.group('sign') == '-':
                match_value = -match_value

        self.populate_constructor_dict(constructor_dict, match_value)
        match_length = m.end() - m.start()
        return True, region.slice(region.start + match_length, region.end)

    def is_numeric_at_end(self):
        return len(self.separator) == 0

    def __hash__(self):
        return hash(self.separator) ^ hash(self.zero_is_z) ^ hash(self.allow_numeric_zero) ^ hash(self.matched_part)

    
class StringDateTimeFormatPart(DateTimeFormatPart):
    __slots__ = ('numbers_to_strings', 'strings_to_numbers', 'parse_regex')
    _cache = {}
    
    def __init__(self, matched_part):
        super().__init__(matched_part)
    
    @classmethod
    def create(cls, numbers_to_strings, matched_part):
        num_to_str = ImmutableDictionary(numbers_to_strings)
        r = cls._cache.get((num_to_str, matched_part), None)
        if r is None:
            r = StringDateTimeFormatPart(matched_part)
            r.numbers_to_strings = num_to_str
            str_to_num = ImmutableDictionary({v: k for k, v in num_to_str.items()})
            r.strings_to_numbers = str_to_num
            r.parse_regex = regex.compile('|'.join(regex.escape(s, special_only=True) for s, v in str_to_num.items()))
            cls._cache[(num_to_str, matched_part)] = r
        return r
    
    def format(self, v):
        if self.matched_part is None:
            return None
        v_matched_part = getattr(v, self.matched_part)
        if v_matched_part is None:
            return None
        return self.numbers_to_strings.get(v_matched_part, None)
    
    def parse(self, region, constructor_dict, diff_from_min_length=None):
        m = self.parse_regex.match(region.content, pos=region.start)
        if m is None:
            return False, region
        match = region.content[m.start():m.end()]
        match_length = len(match)
        self.populate_constructor_dict(constructor_dict, self.strings_to_numbers[match])
        return True, region.slice(region.start + match_length, region.end)


class PartialDateTime(object):
    __slots__ = ('year', 'month', 'day', 'hour', 'minute', 'second', 'millisecond', 
                 'day_of_week', 'quarter', 'ticks', 'hour_in_period', 'period',
                 'day_of_year', 'day_of_week_in_month', 'week_year', 'week_of_year',
                 'time_zone_offset')
    
    
    def __init__(self, year=None, month=None, day=None, hour=None, 
                 minute=None, second=None, millisecond=None, 
                 day_of_week=None, quarter=None, ticks=None,
                 hour_in_period=None, period=None, day_of_year=None,
                 day_of_week_in_month=None,
                 week_year=None, week_of_year=None,
                 time_zone_offset=None,
                 **kwargs):
        self.year = year
        self.month = month
        self.day = day
        self.hour = hour
        self.minute = minute
        self.second = second
        self.millisecond = millisecond
        self.day_of_week = day_of_week
        self.quarter = quarter
        self.ticks = ticks
        self.hour_in_period = hour_in_period
        self.period = period
        self.day_of_year = day_of_year
        self.day_of_week_in_month = day_of_week_in_month
        self.week_year = week_year
        self.week_of_year = week_of_year
        self.time_zone_offset = time_zone_offset


    @staticmethod
    def _day_of_year_to_date(year, day_of_year):
        if not 1 <= day_of_year <= 366:
            return None
        day = day_of_year
        # range's end is exclusive, want month 12 to be an option
        for month in range(1, 13):
            days_in_month = calendar.monthrange(year, month)[1]
            if day <= days_in_month:
                return (month, day)
            day -= days_in_month
        return None


    @staticmethod
    def _day_of_week_in_month_to_day_of_month(year, month, day_of_week, day_of_week_in_month):
        first_dt = datetime.datetime(year, month, 1)
        first_day_of_week = first_dt.isoweekday() % 7
        if day_of_week >= first_day_of_week:
            first_of_day_of_week = day_of_week - first_day_of_week + 1
        else:
            first_of_day_of_week = day_of_week - first_day_of_week + 8
        return first_of_day_of_week + (day_of_week_in_month - 1) * 7


    @staticmethod
    def _week_of_year_to_date(week_year, week_of_year, day_of_week):
        # Based on https://stackoverflow.com/a/33101215
        fourth_jan = datetime.date(week_year, 1, 4)
        _, fourth_jan_week, fourth_jan_day_of_week = fourth_jan.isocalendar()
        if day_of_week == 0:
            day_of_week = 7
        return fourth_jan + datetime.timedelta(days=day_of_week-fourth_jan_day_of_week,
                                               weeks=week_of_year-fourth_jan_week)


    @staticmethod
    def create(year=None, month=None, day=None, hour=None, 
               minute=None, second=None, millisecond=None, 
               day_of_week=None, quarter=None, ticks=None,
               hour_in_period=None, period=None, day_of_year=None,
               day_of_week_in_month=None,
               week_year=None, week_of_year=None,
               time_zone_offset=None,
               **kwargs):
        hour_value_was_24 = False
        if month is not None and not 0 < month <= 12:
            return None
        if day is not None and not 0 < day <= 31:
            return None
        if hour is not None and not 0 <= hour <= 24:
            return None
        elif hour == 24:
            hour_value_was_24 = True
            hour = 0
        if minute is not None and not 0 <= minute < 60:
            return None
        if second is not None and not 0 <= second < 60:
            return None
        if millisecond is not None and not 0 <= millisecond < 1000:
            return None
        if hour_in_period is not None and not 1 <= hour_in_period <= 12:
            return None
        if period is not None and not 0 <= period <= 1:
            return None
        if day_of_year is not None and not 1 <= day_of_year <= 366:
            return None
        if day_of_week_in_month is not None and not 1 <= day_of_week_in_month <= 5:
            return None
        if week_of_year is not None and not 1 <= week_of_year <= 53:
            return None

        if week_of_year == 53 and week_year is not None:
            if datetime.date(week_year, 12, 31).isocalendar()[1] != 53:
                return None

        if year is None and month is not None and day is not None:
            # Using 2004 because it is a leap year.
            if day > calendar.monthrange(2004, month)[1]:
                return None
        elif year is not None and day_of_year is not None:
            computed_date = PartialDateTime._day_of_year_to_date(year, day_of_year)
            if computed_date is None:
                return None
            (computed_month, computed_day) = computed_date
            if month is None:
                month = computed_month
            elif computed_month != month:
                return None
            if day is None:
                day = computed_day
            elif computed_day != day:
                return None
        elif year is not None and month is not None and\
                day_of_week is not None and day_of_week_in_month is not None:
            computed_day = PartialDateTime\
                          ._day_of_week_in_month_to_day_of_month(year, month, day_of_week,
                                                                 day_of_week_in_month)
            if day is None:
                day = computed_day
            elif computed_day != day:
                return None
        elif week_year is not None and week_of_year is not None and day_of_week is not None:
            dt = PartialDateTime._week_of_year_to_date(week_year, week_of_year, day_of_week)

            if year is None:
                year = dt.year
            elif dt.year != year:
                return None

            if month is None:
                month = dt.month
            elif dt.month != month:
                return None

            if day is None:
                day = dt.day
            elif dt.day != day:
                return None

        if year is not None and month is not None and day is not None:
            dt = datetime.datetime(year, month, day)

            if hour_value_was_24:
                dt = dt + datetime.timedelta(days = 1)
                year, month, day = dt.year, dt.month, dt.day
            (computed_week_year, computed_week_of_year, isoweekday) = dt.isocalendar()

            computed_day_of_week = isoweekday % 7
            if day_of_week is None:
                day_of_week = computed_day_of_week
            elif day_of_week != computed_day_of_week:
                return None

            if week_year is None:
                week_year = computed_week_year
            elif week_year != computed_week_year:
                return None

            if week_of_year is None:
                week_of_year = computed_week_of_year
            elif week_of_year != computed_week_of_year:
                return None

            computed_day_of_year = dt.timetuple().tm_yday
            if day_of_year is None:
                day_of_year = computed_day_of_year
            elif computed_day_of_year != day_of_year:
                return None

        if month is not None:
            computed_quarter = ((month - 1) // 3) + 1
            if quarter is None:
                quarter = computed_quarter
            elif quarter != computed_quarter:
                return None

        if day is not None:
            computed_day_of_week_in_month = (day + 6) // 7
            if day_of_week_in_month is None:
                day_of_week_in_month = computed_day_of_week_in_month
            elif day_of_week_in_month != computed_day_of_week_in_month:
                return None

        computed_hour = None
        if hour_in_period is not None and period is not None:
            computed_hour = (hour_in_period % 12) + (12 * period)
            if hour is None:
                hour = computed_hour
            elif hour != computed_hour:
                return None

        if hour is not None and computed_hour is None:
            computed_hour_in_period = hour % 12
            if computed_hour_in_period == 0:
                computed_hour_in_period = 12
            computed_period = 0 if hour < 12 else 1

            if hour_in_period is None:
                hour_in_period = computed_hour_in_period
            elif hour_in_period != computed_hour_in_period:
                return None

            if period is None:
                period = computed_period
            elif period != computed_period:
                return None

        return PartialDateTime(year=year, month=month, day=day,
                               hour=hour, minute=minute, second=second,
                               millisecond=millisecond,
                               day_of_week=day_of_week, quarter=quarter,
                               ticks=ticks,
                               hour_in_period=hour_in_period, period=period,
                               day_of_year=day_of_year,
                               day_of_week_in_month=day_of_week_in_month,
                               week_year=week_year, week_of_year=week_of_year,
                               time_zone_offset=time_zone_offset,
                               **kwargs)
    
    @staticmethod
    def from_datetime(dt):
        return PartialDateTime.create(year=dt.year, month=dt.month, day=dt.day,
                                      hour=dt.hour, minute=dt.minute, second=dt.second,
                                      millisecond=dt.microsecond / 1000)

    def __eq__(self, other):
        if not isinstance(other, PartialDateTime):
            return False
        
        return (self.year == other.year and self.month == other.month and 
                self.day == other.day and self.hour == other.hour and 
                self.minute == other.minute and self.second == other.second and
                self.millisecond == other.millisecond and self.ticks == other.ticks and
                self.day_of_week == other.day_of_week and self.quarter == other.quarter and
                self.hour_in_period == other.hour_in_period and self.period == other.period and
                self.day_of_year == other.day_of_year and
                self.day_of_week_in_month == other.day_of_week_in_month and
                self.week_year == other.week_year and self.week_of_year == other.week_of_year and
                self.time_zone_offset == other.time_zone_offset)
    

class DateTimeFormat(object):
    __slots__ = ('format_parts',)
    
    def __init__(self, *format_parts):
        if len(format_parts) == 0:
            self.format_parts = []
            return
        if isinstance(format_parts[0], list):
            self.format_parts = list(format_parts[0])
        else:
            self.format_parts = list(format_parts)
            
    def parse_to_dict(self, region, constructor_dict=None):
        remaining_region = region
        if constructor_dict is None:
            constructor_dict = {}
        numeric = self.is_numeric()
        if numeric:
            min_length = sum([fp.min_length for fp in self.format_parts])
            max_length = sum([fp.max_length for fp in self.format_parts])
            region_length = len(region)
            if region_length < min_length or region_length > max_length:
                return None
            diff_from_min_length = region_length - min_length
        else:
            diff_from_min_length = None

        for part in self.format_parts:
            (parse_success, remaining_region) = part.parse(remaining_region, constructor_dict, diff_from_min_length=diff_from_min_length)
            if not parse_success:
                return None
        if len(remaining_region) == 0:
            try:
                return constructor_dict
            except:
                return None
        else:
            return None

    def parse(self, region):
        # To handle variable length numeric formats with time zone offsets at the end, parse the end first.
        if len(self.format_parts) > 2 and self.format_parts[-1].is_numeric_at_end() and\
                all([p.is_numeric() for p in self.format_parts[:-1]]):
            last_part = self.format_parts[-1]
            for length in range(min(last_part.max_length, len(region)), last_part.min_length - 1, -1):
                (parse_success, remaining_region) = last_part.parse(region[-length:], constructor_dict)
                if parse_success:
                    if len(remaining_region) > 0:
                        return None
                    constructor_dict = self.parse_to_dict(region[:-length], constructor_dict)
                    if constructor_dict is None:
                        return None
                    return PartialDateTime.create(**constructor_dict)
            return None

        constructor_dict = self.parse_to_dict(region)
        if constructor_dict is None:
            return None
        return PartialDateTime.create(**constructor_dict)
        
    def format(self, partial_date_time):
        if partial_date_time is None:
            return None
        formatted_parts = []
        for format_part in self.format_parts:
            formatted_part = format_part.format(partial_date_time)
            if formatted_part is None:
                return None
            formatted_parts.append(formatted_part)
        return ''.join(formatted_parts)

    def is_numeric(self):
        return len(self.format_parts) > 1 and all([p.is_numeric() for p in self.format_parts])


# FIXME: move these methods to Conditionals python translation module
def if_then_else(b, st, switch):
    if b:
        return st
    else:
        return switch;        


def conditionals_is_null_or_white_space(s):
    return not s or s.isspace()


def conditionals_is_null(s):
    return s is None


def conditionals_is_white_space(s):
    # cannot use string.isspace() because it returns False on empty string
    return not conditionals_is_null(s) and s.strip() == ''


def conditionals_matches(s, r):
    return conditionals_matches_from_ss(str_to_substring(s), r)


def conditionals_matches_from_ss(s, r):
    matches = [] if s is None else s.match(r)
    return len(matches) == 1 and matches[0][0] == s.start and matches[0][1] == s.end


def conditionals_starts_with(s, r):
    return conditionals_starts_with_from_ss(str_to_substring(s), r)


def conditionals_starts_with_from_ss(s, r):
    matches = [] if s is None else s.match(r)
    return len(matches) > 0 and matches[0][0] == s.start


def conditionals_ends_with(s, r):
    return conditionals_ends_with_from_ss(str_to_substring(s), r)


def conditionals_ends_with_from_ss(s, r):
    matches = [] if s is None else s.match(r)
    return len(matches) > 0 and matches[len(matches)-1][1] == s.end


def conditionals_contains(s, r, k):
    return conditionals_contains_from_ss(str_to_substring(s), r, k)


def conditionals_contains_from_ss(s, r, k):
    matches = [] if s is None else s.match(r)
    return len(matches) == k


def conditionals_true():
    return True
# ENDFIXME

def substring_to_str(ss):
    return None if ss is None else ss.get_value()


def str_to_substring(s):
    return None if s is None else Substring(s)


def choose_input(vs, idx):
    return vs[idx]


def lookup_input(vs, idx):
    return vs[idx]


def as_decimal(number):
    if isinstance(number, decimal.Decimal):
        return number
    return decimal.Decimal(number)


def as_partial_date_time(dt):
    if isinstance(dt, PartialDateTime):
        return dt
    if isinstance(dt, datetime.datetime):
        return PartialDateTime.from_datetime(dt)
    raise Exception("Unsupported datetime type: %s" % type(dt))


def relative_position(region, k):
    length = len(region)
    pos = k if k >= 0 else length + k + 1
    return pos if length >= pos >= 0 else None


def add(x, y):
    return x + y if x is not None and y is not None else None


def concat(region1, region2):
    if region1 is None or region2 is None:
        return None
    return region1 + region2


def const_str(s):
    return s


def format_partial_date_time(date_time, output_format):
    return output_format.format(date_time)


def round_partial_date_time(date_time, rounding_spec):
    def get_biggest_time_part(partial_date_time):
        unit = 'day'
        while True:
            unit = get_next_smaller(unit)
            if getattr(partial_date_time, unit) is not None:
                return unit
            if unit == 'millisecond':
                break
        return 'hour'

    def get_next_bigger(unit):
        if unit == 'hour':
            return 'day'
        if unit == 'minute':
            return 'hour'
        if unit == 'second':
            return 'minute'
        if unit == 'millisecond':
            return 'second'
        raise Exception("Unknown next unit: " + unit)

    def get_next_smaller(unit):
        if unit == 'day':
            return 'hour'
        if unit == 'hour':
            return 'minute'
        if unit == 'minute':
            return 'second'
        if unit == 'second':
            return 'millisecond'
        raise Exception("Unknown get_next_smaller unit: " + unit)

    def get_bigger_unit_value(unit):
        if unit == 'hour':
            return 24
        if unit == 'minute' or unit == 'second':
            return 60
        if unit == 'millisecond':
            return 1000
        raise Exception("Unknown get_bigger_unit_value unit: " + unit)

    def get_partial_datetime_only_time(total_milliseconds, max_part_to_keep):
        remaining = total_milliseconds
        milliseconds = int(remaining) % 1000

        seconds = None
        minutes = None
        hours = None

        if max_part_to_keep != 'millisecond':
            remaining = int(remaining / 1000)
            seconds = remaining % 60

            if max_part_to_keep != 'second':
                remaining = int(remaining / 60)
                minutes = remaining % 60

                if max_part_to_keep != 'minute':
                    remaining = int(remaining / 60)
                    hours = remaining % 24

        return PartialDateTime.create(millisecond=milliseconds, second=seconds, minute=minutes, hour=hours) 

    def get_milliseconds_for_part(part):
        scale = 1
        processing_part = 'millisecond'
        while processing_part != part:
            scale = scale * get_bigger_unit_value(processing_part)
            processing_part = get_next_bigger(processing_part)
        return scale

    def get_total_milliseconds(total_date_time):
        adding_unit = 'hour'
        value = 0
        while True:
            value = value * get_bigger_unit_value(adding_unit)
            if getattr(total_date_time, adding_unit) is not None:
                value = value + getattr(total_date_time, adding_unit)

            if adding_unit == 'millisecond':
                break

            adding_unit = get_next_smaller(adding_unit)

        return value

    def add_datetime(add_date_time, amount, unit):
        base_milliseconds = get_total_milliseconds(add_date_time)
        scale = get_milliseconds_for_part(unit)
        total_milliseconds = base_milliseconds + (scale * amount)
        biggest_time_part = get_biggest_time_part(add_date_time)
        if total_milliseconds < 0:
            total_milliseconds += get_milliseconds_for_part(get_next_bigger(biggest_time_part))
        return get_partial_datetime_only_time(total_milliseconds, biggest_time_part)    


    if date_time is None:
        return None

    zero = rounding_spec[0] # PartialDateTime
    delta = rounding_spec[1] # int
    unit = rounding_spec[2] # string (DateTimePart)
    mode = rounding_spec[3] # string (RoundingMode)
    exclude_part = None

    if unit == 'year':
        year = date_time.year
        multiple = delta
        rounded_year = 0
        units = year / delta

        if mode == 'away_from_zero':
            if year < 0:
                rounded_year = math.floor(units) * multiple
            else:
                rounded_year = math.ceil(units) * multiple
        elif mode == 'down':
            rounded_year = math.floor(units) * multiple
        elif mode == 'nearest':
            rounded_year = math.floor(units + 0.5) * multiple
        elif mode == 'toward_zero':
            if year < 0:
                rounded_year = math.ceil(units) * multiple
            else:
                rounded_year = math.floor(units) * multiple
        elif mode == 'up':
            rounded_year = math.ceil(units) * multiple
        elif mode == 'up_or_next':
            rounded_year = (math.floor(units) + 1) * multiple
        else:
            raise NotImplementedError('Unknown rounding mode: %s' % mode)

        return PartialDateTime.create(year=rounded_year) 

    if (len(rounding_spec) > 4):
        exclude_part = rounding_spec[4] # optional string (DateTimePart)
        exclude_amount = rounding_spec[5] # int

    date_time_milliseconds = get_total_milliseconds(date_time)
    milliseconds_for_unit = get_milliseconds_for_part(unit)
    zero_milliseconds = get_total_milliseconds(zero)
    delta_milliseconds = delta * milliseconds_for_unit

    biggest_time_part = get_biggest_time_part(date_time)
    universe_unit = get_next_bigger(biggest_time_part)
    milliseconds_for_universe = get_milliseconds_for_part(universe_unit)

    floor_milliseconds = date_time_milliseconds
    if floor_milliseconds < zero_milliseconds:
        floor_milliseconds = floor_milliseconds + milliseconds_for_universe

    floor_milliseconds = (int((floor_milliseconds - zero_milliseconds) / delta_milliseconds) * delta_milliseconds) + zero_milliseconds

    floor = get_partial_datetime_only_time(floor_milliseconds, biggest_time_part)
    date_time_extended = get_partial_datetime_only_time(date_time_milliseconds, biggest_time_part)

    if mode == 'up_or_next':
        res = add_datetime(floor, delta, unit)
    elif floor == date_time_extended:
        res = date_time_extended;
    elif mode == 'down':
        res = floor
    elif mode == 'up':
        res = add_datetime(floor, delta, unit)
    else:
        raise Exception("Unknown rounding mode: " + mode)

    if exclude_part is not None:
        if mode == 'up' or mode == 'up_or_next':
            res = add_datetime(res, -exclude_amount, exclude_part)
        else:
            raise Exception("Unexpected rounding mode with exclusion: " + mode)

    return res


def parse_partial_date_time(s, input_formats):
    return parse_partial_date_time_from_ss(str_to_substring(s), input_formats)

def parse_partial_date_time_from_ss(region, input_formats):
    res = None
    for input_format in input_formats:
        r = input_format.parse(region)
        if r is not None:
            if res is None:
                res = r
            elif res != r:
                return None
    return res

def _run_rr(region, regex_pair):
    left_matches = region.match(regex_pair[0])
    if len(left_matches) == 0:
        return []
    right_matches = region.match(regex_pair[1])
    if len(right_matches) == 0:
        return []
    right_match_begins = set(x[0] for x in right_matches)
    return list(map(lambda t: t[1], filter(lambda t: t[1] in right_match_begins, left_matches)))
    

def regex_position(s, regex_pair, k):
    return regex_position_from_ss(str_to_substring(s), regex_pair, k)


def regex_position_relative(s, regex_pair, k):
    return regex_position_relative_from_ss(str_to_substring(s), regex_pair, k)


def regex_position_from_ss(region, regex_pair, k):
    if k == 0:
        raise IndexError('Zero is invalid index for regex position.')
    run = _run_rr(region, regex_pair)
    run_len = len(run)
    if run_len < abs(k):
        raise IndexError('No match of regular expression %s in region %s with index %d.' % (regex_pair, region, k))
    return run[k - 1] if k > 0 else run[run_len + k]


def regex_position_relative_from_ss(region, regex_pair, k):
    return regex_position_from_ss(region, regex_pair, k) - region.start


def r_sub_str(region, optional_index):
    if optional_index is None:
        return None
    return region.slice(region.start + optional_index, region.end)


def sub_str(region, position_pair):
    left = position_pair[0]
    right = position_pair[1]
    region_length = len(region)
    if left is None or right is None or left > region_length or right > region_length or left > right:
        return None
    return region.slice(region.start + left, region.start + right)


def external_extractor_position_pair(s, extractor_lambda, k):
    return external_extractor_position_pair_from_ss(str_to_substring(s), extractor_lambda, k)


def external_extractor_position_pair_from_ss(region, extractor_lambda, k):
    matches = extractor_lambda(region)
    k = (len(matches) + k) if k < 0 else k
    return None if k >= len(matches) else matches[k]


def external_extractor_position_pair_regex_extractor(regex):
    return lambda r: r.match(regex)


def to_lowercase(actual):
    return actual.lower()


def to_uppercase(actual):
    return actual.upper()


_titlecase_conversion_regex = regex.compile('(\p{Ll})(\p{L}*)')
def _titlecase_conversion_lambda(m):
    return m.group(1).upper() + m.group(2)


def to_simple_title_case(actual):
    return _titlecase_conversion_regex.sub(_titlecase_conversion_lambda, actual.lower())


class NumberFormatDetails(object):
    __slots__ = ('decimal_mark_char', 'separator_char', 'separated_section_sizes', 'scale', 'currency_symbol',
                 'allow_parse_parens_as_negative')

    def __init__(self, decimal_mark_char, separator_char=None, separated_section_sizes=None, scale=None, currency_symbol=None):
        if decimal_mark_char is None:
            raise Exception("NumberFormatDetails must have a decimal_mark_char.")
        self.decimal_mark_char = decimal_mark_char
        self.separator_char = separator_char
        self.separated_section_sizes = separated_section_sizes
        self.scale = scale
        self.currency_symbol = currency_symbol
        self.allow_parse_parens_as_negative = False # To be supported in future

    def __eq__(self, other):
        if not isinstance(other, NumberFormatDetails):
            return False

        return (self.decimal_mark_char == other.decimal_mark_char
                and self.separator_char == other.separator_char
                and self.separated_section_sizes == other.separated_section_sizes
                and self.scale == other.scale
                and self.currency_symbol == other.currency_symbol)


class NumberFormat(object):
    __slots__ = ('min_trailing_zeros', 'max_trailing_zeros', 'min_trailing_zeros_and_whitespace',
                 'min_leading_zeros', 'min_leading_zeros_and_whitespace',
                 'details')
    
    def __init__(self, min_trailing_zeros=None, max_trailing_zeros=None, min_trailing_zeros_and_whitespace=None,
                 min_leading_zeros=None, min_leading_zeros_and_whitespace=None,
                 details=None, **kwargs):
        self.min_trailing_zeros = min_trailing_zeros
        self.max_trailing_zeros = max_trailing_zeros
        self.min_trailing_zeros_and_whitespace = min_trailing_zeros_and_whitespace
        self.min_leading_zeros = min_leading_zeros
        self.min_leading_zeros_and_whitespace = min_leading_zeros_and_whitespace
        self.details = details
        
    def __eq__(self, other):
        if not isinstance(other, NumberFormat):
            return False
        
        return (self.min_trailing_zeros == other.min_trailing_zeros
                and self.max_trailing_zeros == other.max_trailing_zeros
                and self.min_trailing_zeros_and_whitespace == other.min_trailing_zeros_and_whitespace
                and self.min_leading_zeros == other.min_leading_zeros
                and self.min_leading_zeros_and_whitespace == other.min_leading_zeros_and_whitespace
                and self.details == other.details)


def round_number(number, rounding_spec):
    zero = rounding_spec[0]
    delta = rounding_spec[1]
    mode = rounding_spec[2]

    floor = math.floor((number - zero) / delta) * delta + zero
    if mode == 'up_or_next':
        res = floor + delta
    elif floor == number:
        res = number
    elif mode == 'down':
        res = floor
    elif mode == 'up':
        res = floor + delta
    elif mode == 'toward_zero':
        if number < 0:
            res = floor + delta
        else:
            res = floor
    elif mode == 'away_from_zero':
        if number < 0:
            res = floor
        else:
            res = floor + delta
    elif (number - floor) * 2 < delta:
        res = floor
    else:
        res = floor + delta
    return res.normalize()


def format_number(number, format):
    """
    Formats a number according to the given format descriptor
    :param number: The number to be formatted
    :param format: The format descriptor
    :return: The number formatted according to the format descriptor

    >>> f = build_number_format(None, 0, None, None, None, NumberFormatDetails(".", None, None, decimal.Decimal("1")))
    >>> format_number(decimal.Decimal("-243.4"), f)
    '-243'
    >>> f = build_number_format(None, 0, None, None, None, NumberFormatDetails(".", ",", [3], decimal.Decimal("1")))
    >>> format_number(decimal.Decimal("1234567890"), f)
    '1,234,567,890'
    """

    if format.details.scale is not None:
        # Multiply or divide depending on which treat trailing zeros correctly:
        # >>> Decimal('1.00') * Decimal('0.01')
        # Decimal('0.0100')  # GOOD
        # >>> Decimal('1.00') * Decimal('100')
        # Decimal('100.00')  # BAD
        # >>> Decimal('1.00') / (1 / Decimal('100'))
        # Decimal('100')     # GOOD
        if format.details.scale < 1:
            number *= format.details.scale
        else:
            number /= (1 / format.details.scale)
    if format.max_trailing_zeros is not None:
        pad_string = '1.' + ('0' * format.max_trailing_zeros)
        number_temp = number.quantize(decimal.Decimal(pad_string), rounding=decimal.ROUND_HALF_UP)
        if len(str(number_temp)) < len(str(number)):
            number = number_temp

    int_part = abs(math.trunc(abs(number)))
    dec_part = abs(abs(number) - int_part)
    int_to_string = str(int_part)

    if (format.details.separator_char is not None and
            format.details.separated_section_sizes is not None and
            0 < format.details.separated_section_sizes[0] < len(int_to_string)):
        # Keep section list reversed because appending to list is faster than prepending.
        rev_section_list = []
        end_int_to_string = len(int_to_string)
        for size in itertools.chain(format.details.separated_section_sizes,
                                    itertools.repeat(format.details.separated_section_sizes[-1])):
            if size == 0 or end_int_to_string < size:
                if end_int_to_string != 0:
                    rev_section_list.append(int_to_string[0:end_int_to_string])
                    end_int_to_string = 0
                break
            else:
                rev_section_list.append(int_to_string[end_int_to_string - size:end_int_to_string])
                end_int_to_string -= size

        int_to_string = format.details.separator_char.join(reversed(rev_section_list))

    if format.min_leading_zeros is not None:
        int_to_string = int_to_string.rjust(format.min_leading_zeros, '0')
    if format.details.currency_symbol is not None:
        int_to_string = format.details.currency_symbol + int_to_string
    if number < 0:
        int_to_string = '-' + int_to_string
    if format.min_leading_zeros_and_whitespace is not None:
        int_to_string = int_to_string.rjust(format.min_leading_zeros_and_whitespace, ' ')

    if (format.min_trailing_zeros is None and
            format.min_trailing_zeros_and_whitespace is None and
            dec_part == 0 and '.' not in str(dec_part)):
        dec_to_string = ''
    else:
        dec_to_string = str(dec_part)[2:]
        if format.min_trailing_zeros is not None:
            dec_to_string = dec_to_string.ljust(format.min_trailing_zeros, '0')
        if format.min_trailing_zeros_and_whitespace is not None:
            dec_to_string = dec_to_string.ljust(format.min_trailing_zeros_and_whitespace, ' ')
        dec_to_string = format.details.decimal_mark_char + dec_to_string

    return int_to_string + dec_to_string


def build_number_format(min_trailing_zeros, max_trailing_zeros,
                                            min_trailing_zeros_and_whitespace,
                                            min_leading_zeros, min_leading_zeros_and_whitespace,
                                            details):
    return NumberFormat(min_trailing_zeros, max_trailing_zeros, min_trailing_zeros_and_whitespace,
                        min_leading_zeros, min_leading_zeros_and_whitespace,
                        details)


def parse_number(num_str, number_format_details):
    """
    Parses a number. number_format_details contains information about what input number formats are
    allowed. For example, what, if any, currency symbol may be used, and whether . is used as a
    decimal separator (as is common in America) or as a thousands separator (as is common in Europe).

    >>> default_details = NumberFormatDetails(decimal_mark_char='.', separator_char=',')
    >>> space_details = NumberFormatDetails(decimal_mark_char='.', separator_char=' ')
    >>> dollar_details = NumberFormatDetails(decimal_mark_char='.', separator_char=',', currency_symbol='$')
    >>> parse_number("", default_details)
    >>> parse_number("1", default_details)
    Decimal('1')
    >>> parse_number("+1", default_details)
    Decimal('1')
    >>> parse_number("1+", default_details)
    Decimal('1')
    >>> parse_number("0", default_details)
    Decimal('0')
    >>> parse_number(" 1 ", default_details)
    Decimal('1')
    >>> parse_number("\uff19", default_details)
    Decimal('9')
    >>> d = parse_number("43e3", default_details)
    >>> d == decimal.Decimal('43000')
    True
    >>> parse_number("3E-1", default_details)
    Decimal('0.3')
    >>> parse_number("NaN", default_details)
    >>> parse_number("Inf", default_details)
    >>> parse_number(".12", default_details)
    Decimal('0.12')
    >>> parse_number(".43E2", default_details)
    Decimal('43')
    >>> parse_number("(.43E2)", default_details)
    >>> parse_number("-.43E2", default_details)
    Decimal('-43')
    >>> parse_number("-$.43E2", dollar_details)
    Decimal('-43')
    >>> parse_number("$.43E2", dollar_details)
    Decimal('43')
    >>> parse_number(".43E2$", dollar_details)
    Decimal('43')
    >>> parse_number("43$", dollar_details)
    Decimal('43')
    >>> parse_number("43 $", dollar_details)
    Decimal('43')
    >>> parse_number("43- $", dollar_details)
    Decimal('-43')
    >>> parse_number("43 $-", dollar_details)
    Decimal('-43')
    >>> parse_number("43 $+", dollar_details)
    Decimal('43')
    >>> parse_number("1,234", default_details)
    Decimal('1234')
    >>> parse_number("1 234", space_details)
    Decimal('1234')
    """
    try:
        if number_format_details.separator_char is not None:
            num_str = num_str.replace(number_format_details.separator_char, '')
        if number_format_details.currency_symbol is not None:
            # delete currency symbol only from start of string
            curr_sym = number_format_details.currency_symbol
            if num_str.find(curr_sym, 0, 1 + len(curr_sym)) in [0, 1]:
                num_str = num_str.replace(curr_sym, '', 1).strip()
            elif num_str.rfind(curr_sym, len(num_str) - 1 - len(curr_sym)) >= len(num_str) - 2:
                # Remove one instance of currency symbol; if there's multiple,
                #   that's an error and decimal.Decimal() will throw an exception.
                num_str = num_str.replace(curr_sym, '', 1).strip()
        # support parens for negative
        if number_format_details.allow_parse_parens_as_negative and num_str[0] == '(' and num_str[-1] == ')':
            num_str = '-%s' % num_str[1:-1]
        # support trailing sign
        elif num_str[-1] in ['-', '+']:
            num_str = '%s%s' % (num_str[-1], num_str[:-1])
        res = decimal.Decimal(num_str)
    except:
        return None
    if res.is_infinite() or res.is_nan():
        return None
    return res

def lookup(x, lookup_dictionary):
    key = Optional.some(x) if x is not None else Optional.nothing()
    return lookup_dictionary.get(key, None)


# the input converter for ttext programs.
def _typed_repr(arg_tuple):
    x = arg_tuple[0]
    type = arg_tuple[1]
    if type == 'str':
        yield repr(x)
    elif type == 'decimal':
        yield 'decimal.Decimal(%s)' % repr(x)
    elif type == 'int':
        yield 'int(%s)' % repr(x)
    elif type == 'float':
        yield 'float(%s)' % repr(x)
    elif type == 'datetime':
        yield 'datetime.datetime(**%s)' % repr({key: int(value) for key, value in x.items()})
    elif type == "typed_repr_dict":
        yield '{' + ', '.join({'%s: %s' % (repr(key), list(_typed_repr(value))[0]) for key, value in x.items()}) + '}'
    elif type == "python_dict":
        yield '{' + ', '.join({'%s: %s' % (repr(key), value) for key, value in x.items()}) + '}'
    elif type == "python":
        yield x
    else:
        raise NotImplementedError("Unexpected type %s in _typed_repr()." % type)

