class TimeToken(EntityToken):
    __slots__ = ('time',)

    class_id = EntityType.Time

    def __init__(self, source, start, end, time):
        super().__init__(source, start, end)
        self.time = time

    def __eq__(self, other):
        if other is None:
            return False
        if other is self:
            return True
        return type(self) == type(other) and self.time == other.time

    def __hash__(self):
        return hash(self.time) ^ hash(str(type(self)))


class TimeTokenizer(RegexBasedTokenizer):
    """
    >>> t = TimeTokenizer()
    >>> results = []
    >>> for test in _time_tokenizer_test_cases():
    ...     data = test[1]
    ...     time = test[0]
    ...     tokens = list(filter(lambda tok: (type(tok) == TimeToken and tok.start == 0 and tok.end == len(data)),
    ...                          t.match_tokens(data)))
    ...     results.append(len(tokens) >= 1)
    >>> failing_test_indices = list(filter(lambda i: not results[i], range(len(results))))
    >>> failing_test_indices
    []
    >>> len(failing_test_indices)
    0
    """
    left_context_pattern_string = r"(?:^|[^\-\d\.\:])"
    am_pm_pattern_string = r"(?:(?<AmPm>[AaPp])\.?(?:[Mm]\.?)?)?"
    two_digits_pattern_string = r"\d{1,2}"
    whitespace_pattern_string = r"\s*"

    hours_group_name = "Hours"
    minutes_group_name = "Minutes"
    seconds_group_name = "Seconds"
    milliseconds_group_name = "Milliseconds"
    am_pm_group_name = "AmPm"

    hours_and_minutes_pattern_string = r"(?<{0}>{1}){2}\:{3}(?<{4}>{5}){6}{7}".format(
        hours_group_name,
        two_digits_pattern_string,
        whitespace_pattern_string,
        whitespace_pattern_string,
        minutes_group_name,
        two_digits_pattern_string,
        whitespace_pattern_string,
        am_pm_pattern_string
    )

    hours_minutes_and_seconds_pattern_string = r"(?<{0}>{1}){2}\:{3}(?<{4}>{5}){6}\:{7}(?<{8}>{9}){10}{11}".format(
        hours_group_name,
        two_digits_pattern_string,
        whitespace_pattern_string,
        whitespace_pattern_string,
        minutes_group_name,
        two_digits_pattern_string,
        whitespace_pattern_string,
        whitespace_pattern_string,
        seconds_group_name,
        two_digits_pattern_string,
        whitespace_pattern_string,
        am_pm_pattern_string
    )

    hours_minutes_seconds_and_millis_pattern_string = \
        r"(?<{0}>{1}){2}\:{3}(?<{4}>{5}){6}\:{7}(?<{8}>{9})\.(?<{10}>\d+){11}{12}".format(
            hours_group_name,
            two_digits_pattern_string,
            whitespace_pattern_string,
            whitespace_pattern_string,
            minutes_group_name,
            two_digits_pattern_string,
            whitespace_pattern_string,
            whitespace_pattern_string,
            seconds_group_name,
            two_digits_pattern_string,
            milliseconds_group_name,
            whitespace_pattern_string,
            am_pm_pattern_string
        )
    
    minutes_seconds_and_millis_pattern_string = \
        r"(?<{0}>{1}){2}\:{3}(?<{4}>{5})\.(?<{6}>\d+)".format(
            minutes_group_name,
            two_digits_pattern_string,
            whitespace_pattern_string,
            whitespace_pattern_string,
            seconds_group_name,
            two_digits_pattern_string,
            milliseconds_group_name,
        )

    def __init__(self):
        super().__init__(OverlapStrategy.SUBSUMPTION,
                         TokenPattern(TimeTokenizer.hours_and_minutes_pattern_string,
                                      TimeTokenizer.left_context_pattern_string),
                         TokenPattern(TimeTokenizer.hours_minutes_and_seconds_pattern_string,
                                      TimeTokenizer.left_context_pattern_string),
                         TokenPattern(TimeTokenizer.hours_minutes_seconds_and_millis_pattern_string,
                                      TimeTokenizer.left_context_pattern_string),
                         TokenPattern(TimeTokenizer.minutes_seconds_and_millis_pattern_string,
                                      TimeTokenizer.left_context_pattern_string))

    def process_matches(self, matches):
        for m in matches:
            full_match = m.full_match
            s = m.source
            hours_span = RegexBasedTokenizer.get_group_span(full_match, TimeTokenizer.hours_group_name)
            minutes_span = RegexBasedTokenizer.get_group_span(full_match, TimeTokenizer.minutes_group_name)
            seconds_span = RegexBasedTokenizer.get_group_span(full_match, TimeTokenizer.seconds_group_name)
            milliseconds_span = RegexBasedTokenizer.get_group_span(full_match, TimeTokenizer.milliseconds_group_name)
            am_pm_span = RegexBasedTokenizer.get_group_span(full_match, TimeTokenizer.am_pm_group_name)

            hours_str = s[hours_span[0]:hours_span[1]] if hours_span is not None else "0"
            minutes_str = s[minutes_span[0]:minutes_span[1]] if minutes_span is not None else "0"
            seconds_str = s[seconds_span[0]:seconds_span[1]] if seconds_span is not None else "0"
            milliseconds_str = s[milliseconds_span[0]:milliseconds_span[1]] if milliseconds_span is not None else "0"
            am_pm_str = s[am_pm_span[0]:am_pm_span[1]] if am_pm_span is not None else ""
            if len(am_pm_str) > 0:
                am_pm_str = "AM" if am_pm_str.lower()[0] == 'a' else "PM"

            hours = RegexBasedTokenizer.try_parse_int(hours_str)
            minutes = RegexBasedTokenizer.try_parse_int(minutes_str)
            seconds = RegexBasedTokenizer.try_parse_int(seconds_str)
            milliseconds = RegexBasedTokenizer.try_parse_int(milliseconds_str)

            if hours < 12 and am_pm_str == "PM":
                hours += 12

            if ((hours < 0 or hours >= 24) or
                    (minutes < 0 or minutes >= 60) or
                    (seconds < 0 or seconds >= 60) or
                    (milliseconds < 0 or milliseconds >= 1000000)):
                continue

            yield TimeToken(m.source, m.start, m.end, datetime.time(hours, minutes, seconds, milliseconds))


# testing code
def _time_tokenizer_test_cases():
    yield (datetime.time(12, 1, 32, 993000), "12:01 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1")
    yield (datetime.time(10, 10, 10, 10000), "10:10")
    yield (datetime.time(16, 15, 16, 16000), "4:15")
    yield (datetime.time(0, 10, 10, 93000), "12:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01")
    yield (datetime.time(10, 10, 10, 10000), "10:10")
    yield (datetime.time(16, 15, 16, 16000), "4:15")
    yield (datetime.time(0, 10, 10, 93000), "12:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "4:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1")
    yield (datetime.time(10, 10, 10, 10000), "10:10")
    yield (datetime.time(16, 15, 16, 16000), "04:15")
    yield (datetime.time(0, 10, 10, 93000), "12:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01")
    yield (datetime.time(10, 10, 10, 10000), "10:10")
    yield (datetime.time(16, 15, 16, 16000), "04:15")
    yield (datetime.time(0, 10, 10, 93000), "12:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "04:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "12:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1")
    yield (datetime.time(10, 10, 10, 10000), "10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15")
    yield (datetime.time(0, 10, 10, 93000), "0:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01")
    yield (datetime.time(10, 10, 10, 10000), "10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15")
    yield (datetime.time(0, 10, 10, 93000), "0:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "0:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1")
    yield (datetime.time(10, 10, 10, 10000), "10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15")
    yield (datetime.time(0, 10, 10, 93000), "00:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:1:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01")
    yield (datetime.time(10, 10, 10, 10000), "10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15")
    yield (datetime.time(0, 10, 10, 93000), "00:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10 AM")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 P")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 A")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 P")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000 A")
    yield (datetime.time(12, 1, 32, 993000), "12:01:32.993000 PM")
    yield (datetime.time(10, 10, 10, 10000), "10:10:10.010000 AM")
    yield (datetime.time(16, 15, 16, 16000), "16:15:16.016000 PM")
    yield (datetime.time(0, 10, 10, 93000), "00:10:10.093000 AM")

if __name__ == '__main__':
    import doctest
    doctest.testmod()
