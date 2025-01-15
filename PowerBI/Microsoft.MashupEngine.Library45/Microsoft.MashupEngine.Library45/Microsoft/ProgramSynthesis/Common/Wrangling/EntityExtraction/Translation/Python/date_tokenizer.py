class DateToken(EntityToken):
    __slots__ = ('date',)

    class_id = EntityType.Date

    def __init__(self, source, start, end, date_value):
        super().__init__(source, start, end)
        self.date = date_value

    def __eq__(self, other):
        if other is None:
            return False
        if other is self:
            return True
        return type(other) == type(self) and self.date == other.date

    def __hash__(self):
        return hash(self.date) ^ hash(str(type(self)))


class DateTokenizer(RegexBasedTokenizer):
    """
    >>> t = DateTokenizer()
    >>> results = []
    >>> for test in _date_tokenizer_test_cases():
    ...     data = test[1]
    ...     date = test[0]
    ...     tokens = list(filter(lambda tok: type(tok) == DateToken and tok.date == date, t.match_tokens(data)))
    ...     results.append(len(tokens) >= 1)
    >>> failing_test_indices = list(filter(lambda i: not results[i], range(len(results))))
    >>> failing_test_indices
    []
    >>> len(failing_test_indices)
    0
    """

    left_context_pattern_string = r'(?:^|[^\d\.\-])'
    right_context_pattern_string = r'(?:$|[^\d\.\-])'
    month_first_left_context_pattern_string = r'(?:^|[^\d\.\-\p{L}])'
    one_or_two_digits_pattern_string = r'\d{1,2}'
    two_digits_pattern_string = r'\d{2}'
    separator_pattern_string = r'\s*[\.\-\\/\s]\s*'
    two_to_four_digits_pattern_string = r'\d{2,4}'
    four_digits_pattern_string = r'\d{4}'

    first_numeric_group_name = 'First'
    second_numeric_group_name = 'Second'
    third_numeric_group_name = 'Third'
    month_group_name = 'Month'

    month_name_to_month_num = {calendar.month_name[i]: i for i in range(1, 13)}
    month_abbr_to_month_num = {calendar.month_abbr[i]: i for i in range(1, 13)}
    month_name_to_month_num.update(month_abbr_to_month_num)

    separated_numeric_date_pattern_string = \
        r"(?<{0}>{1}){2}(?<{3}>{4}){5}'?(?<{6}>{7})".format(first_numeric_group_name,
                                                            one_or_two_digits_pattern_string,
                                                            separator_pattern_string,
                                                            second_numeric_group_name,
                                                            one_or_two_digits_pattern_string,
                                                            separator_pattern_string,
                                                            third_numeric_group_name,
                                                            two_to_four_digits_pattern_string)

    non_separated_numeric_date_pattern_string_1 = \
        r"(?<{0}>{1})(?<{2}>{3})(?<{4}>{5})".format(first_numeric_group_name,
                                                    two_digits_pattern_string,
                                                    second_numeric_group_name,
                                                    two_digits_pattern_string,
                                                    third_numeric_group_name,
                                                    four_digits_pattern_string)

    non_separated_numeric_date_pattern_string_2 = \
        r"(?<{0}>{1})(?<{2}>{3})(?<{4}>{5})".format(first_numeric_group_name,
                                                    four_digits_pattern_string,
                                                    second_numeric_group_name,
                                                    two_digits_pattern_string,
                                                    third_numeric_group_name,
                                                    two_digits_pattern_string)

    month_first_date_pattern_string = \
        r"(?<{0}>\p{{L}}{{3,20}})\s*,?[\.\-\\/\s]\s*(?<{1}>{2})\s*[\.\-\\/\s]?,?\s*'?(?<{3}>{4})".format(
            month_group_name,
            first_numeric_group_name,
            one_or_two_digits_pattern_string,
            second_numeric_group_name,
            two_to_four_digits_pattern_string)

    month_second_date_pattern_string = r"(?<{0}>{1}){2}(?<{3}>\p{{L}}{{3,20}}){4}'?(?<{5}>{6})".format(
        first_numeric_group_name,
        one_or_two_digits_pattern_string,
        separator_pattern_string,
        month_group_name,
        separator_pattern_string,
        second_numeric_group_name,
        two_to_four_digits_pattern_string)

    year_first_date_pattern_string = r"(?<{0}>{1}){2}(?<{3}>{4}){5}(?<{6}>{7})".format(
        first_numeric_group_name,
        two_to_four_digits_pattern_string,
        separator_pattern_string,
        second_numeric_group_name,
        one_or_two_digits_pattern_string,
        separator_pattern_string,
        third_numeric_group_name,
        one_or_two_digits_pattern_string)

    year_first_named_month_pattern_string = r"(?<{0}>{1}){2}(?<{3}>\p{{L}}{{3,20}}){4}(?<{5}>{6})".format(
        first_numeric_group_name,
        two_to_four_digits_pattern_string,
        separator_pattern_string,
        month_group_name,
        separator_pattern_string,
        second_numeric_group_name,
        one_or_two_digits_pattern_string)

    def __init__(self):
        super().__init__(OverlapStrategy.SUBSUMPTION,
                         TokenPattern(DateTokenizer.separated_numeric_date_pattern_string,
                                      DateTokenizer.left_context_pattern_string,
                                      DateTokenizer.right_context_pattern_string),
                         TokenPattern(DateTokenizer.non_separated_numeric_date_pattern_string_1,
                                      DateTokenizer.left_context_pattern_string,
                                      DateTokenizer.right_context_pattern_string),
                         TokenPattern(DateTokenizer.non_separated_numeric_date_pattern_string_2,
                                      DateTokenizer.left_context_pattern_string,
                                      DateTokenizer.right_context_pattern_string),
                         TokenPattern(DateTokenizer.month_first_date_pattern_string,
                                      DateTokenizer.month_first_left_context_pattern_string,
                                      DateTokenizer.right_context_pattern_string),
                         TokenPattern(DateTokenizer.month_second_date_pattern_string,
                                      DateTokenizer.left_context_pattern_string,
                                      DateTokenizer.right_context_pattern_string),
                         TokenPattern(DateTokenizer.year_first_date_pattern_string,
                                      DateTokenizer.left_context_pattern_string,
                                      DateTokenizer.right_context_pattern_string),
                         TokenPattern(DateTokenizer.year_first_named_month_pattern_string,
                                      DateTokenizer.left_context_pattern_string,
                                      DateTokenizer.right_context_pattern_string))

    @staticmethod
    def resolve_from_int_components(components, m):
        component_indices = list(range(len(components)))
        day_candidate_indices = filter(lambda idx: 0 < components[idx] <= 31, component_indices)
        month_candidate_indices = filter(lambda idx: 0 < components[idx] <= 12, component_indices)
        year_candidate_indices = filter(lambda idx: components[idx] > 0, component_indices)

        product_indices = itertools.product(year_candidate_indices,
                                            month_candidate_indices,
                                            day_candidate_indices)

        product_indices = filter(lambda t2: (len(set(t2)) == len(t2)
                                             and (calendar.monthrange(components[t2[0]], components[t2[1]])[1] >=
                                                  components[t2[2]])), product_indices)

        for tup in product_indices:
            day = components[tup[2]]
            month = components[tup[1]]
            year = components[tup[0]]
            yield DateToken(m.source, m.start, m.end, datetime.date(year, month, day))

            if year < 100:
                yield DateToken(m.source, m.start, m.end, datetime.date(year + 1900, month, day))
                yield DateToken(m.source, m.start, m.end, datetime.date(year + 2000, month, day))

    @staticmethod
    def resolve_from_str_components(components, m):
        component_indices = list(range(len(components)))
        components_as_int = [DateTokenizer.try_parse_int(x) for x in components]
        month_candidate_indices = filter(lambda idx: components_as_int[idx] is None, component_indices)
        day_candidate_indices = filter(lambda idx: (components_as_int[idx] is not None
                                                    and 0 < components_as_int[idx] <= 31),
                                       component_indices)
        year_candidate_indices = filter(lambda idx: components_as_int[idx] is not None and components_as_int[idx] > 0,
                                        component_indices)
        product_indices = itertools.product(year_candidate_indices, month_candidate_indices, day_candidate_indices)
        product_indices = filter(lambda tup: (len(set(tup)) == len(tup)), product_indices)
        for idx_tuple in product_indices:
            indexed_components = [components_as_int[x] for x in idx_tuple]
            month = DateTokenizer.month_name_to_month_num.get(components[1], None)
            if month is None:
                continue
            yield DateToken(m.source, m.start, m.end, datetime.date(indexed_components[0], month,
                                                                    indexed_components[2]))

            if indexed_components[0] < 100:
                yield DateToken(m.source, m.start, m.end, datetime.date(indexed_components[0] + 1900, month,
                                                                        indexed_components[2]))
                yield DateToken(m.source, m.start, m.end, datetime.date(indexed_components[0] + 2000, month,
                                                                        indexed_components[2]))

    def process_matches(self, matches):
        for m in matches:
            month_group_span = DateTokenizer.get_group_span(m.full_match, DateTokenizer.month_group_name)
            s = m.full_match.string
            if month_group_span is not None:
                first_numeric_span = DateTokenizer.get_group_span(m.full_match, DateTokenizer.first_numeric_group_name)
                second_numeric_span = DateTokenizer.get_group_span(m.full_match,
                                                                   DateTokenizer.second_numeric_group_name)

                components = [s[first_numeric_span[0]:first_numeric_span[1]],
                              s[month_group_span[0]:month_group_span[1]],
                              s[second_numeric_span[0]:second_numeric_span[1]]]
                yield from DateTokenizer.resolve_from_str_components(components, m)
            else:
                first_numeric_span = DateTokenizer.get_group_span(m.full_match, DateTokenizer.first_numeric_group_name)
                second_numeric_span = DateTokenizer.get_group_span(m.full_match,
                                                                   DateTokenizer.second_numeric_group_name)
                third_numeric_span = DateTokenizer.get_group_span(m.full_match, DateTokenizer.third_numeric_group_name)

                if first_numeric_span is None or second_numeric_span is None or third_numeric_span is None:
                    return

                components = [s[first_numeric_span[0]:first_numeric_span[1]],
                              s[second_numeric_span[0]:second_numeric_span[1]],
                              s[third_numeric_span[0]:third_numeric_span[1]]]

                components = [DateTokenizer.try_parse_int(x) for x in components]
                yield from DateTokenizer.resolve_from_int_components(components, m)


# testing code
def _date_tokenizer_test_cases():
    yield (datetime.date(2017, 1, 13), "1/13/2017")
    yield (datetime.date(2017, 12, 12), "12/12/2017")
    yield (datetime.date(2017, 1, 1), "1/1/2017")
    yield (datetime.date(2016, 10, 11), "10/11/2016")
    yield (datetime.date(2017, 1, 13), "Friday, January 13, 2017")
    yield (datetime.date(2017, 12, 12), "Tuesday, December 12, 2017")
    yield (datetime.date(2017, 1, 1), "Sunday, January 1, 2017")
    yield (datetime.date(2016, 10, 11), "Tuesday, October 11, 2016")
    yield (datetime.date(2017, 1, 13), "Friday, January 13, 2017 12:00 AM")
    yield (datetime.date(2017, 12, 12), "Tuesday, December 12, 2017 12:00 AM")
    yield (datetime.date(2017, 1, 1), "Sunday, January 1, 2017 12:00 AM")
    yield (datetime.date(2016, 10, 11), "Tuesday, October 11, 2016 12:00 AM")
    yield (datetime.date(2017, 1, 13), "Friday, January 13, 2017 12:00:00 AM")
    yield (datetime.date(2017, 12, 12), "Tuesday, December 12, 2017 12:00:00 AM")
    yield (datetime.date(2017, 1, 1), "Sunday, January 1, 2017 12:00:00 AM")
    yield (datetime.date(2016, 10, 11), "Tuesday, October 11, 2016 12:00:00 AM")
    yield (datetime.date(2017, 1, 13), "1/13/2017 12:00 AM")
    yield (datetime.date(2017, 12, 12), "12/12/2017 12:00 AM")
    yield (datetime.date(2017, 1, 1), "1/1/2017 12:00 AM")
    yield (datetime.date(2016, 10, 11), "10/11/2016 12:00 AM")
    yield (datetime.date(2017, 1, 13), "1/13/2017 12:00:00 AM")
    yield (datetime.date(2017, 12, 12), "12/12/2017 12:00:00 AM")
    yield (datetime.date(2017, 1, 1), "1/1/2017 12:00:00 AM")
    yield (datetime.date(2016, 10, 11), "10/11/2016 12:00:00 AM")
    yield (datetime.date(2017, 1, 13), "Fri, 13 Jan 2017 00:00:00 GMT")
    yield (datetime.date(2017, 12, 12), "Tue, 12 Dec 2017 00:00:00 GMT")
    yield (datetime.date(2017, 1, 1), "Sun, 01 Jan 2017 00:00:00 GMT")
    yield (datetime.date(2016, 10, 11), "Tue, 11 Oct 2016 00:00:00 GMT")
    yield (datetime.date(2017, 1, 13), "Fri, 13 Jan 2017 00:00:00 GMT")
    yield (datetime.date(2017, 12, 12), "Tue, 12 Dec 2017 00:00:00 GMT")
    yield (datetime.date(2017, 1, 1), "Sun, 01 Jan 2017 00:00:00 GMT")
    yield (datetime.date(2016, 10, 11), "Tue, 11 Oct 2016 00:00:00 GMT")
    yield (datetime.date(2017, 1, 13), "2017-01-13T00:00:00")
    yield (datetime.date(2017, 12, 12), "2017-12-12T00:00:00")
    yield (datetime.date(2017, 1, 1), "2017-01-01T00:00:00")
    yield (datetime.date(2016, 10, 11), "2016-10-11T00:00:00")
    yield (datetime.date(2017, 1, 13), "2017-01-13 00:00:00Z")
    yield (datetime.date(2017, 12, 12), "2017-12-12 00:00:00Z")
    yield (datetime.date(2017, 1, 1), "2017-01-01 00:00:00Z")
    yield (datetime.date(2016, 10, 11), "2016-10-11 00:00:00Z")
    yield (datetime.date(2017, 1, 13), "Friday, January 13, 2017 8:00:00 AM")
    yield (datetime.date(2017, 12, 12), "Tuesday, December 12, 2017 8:00:00 AM")
    yield (datetime.date(2017, 1, 1), "Sunday, January 1, 2017 8:00:00 AM")
    yield (datetime.date(2016, 10, 11), "Tuesday, October 11, 2016 7:00:00 AM")
    yield (datetime.date(2017, 1, 13), "13-1-17")
    yield (datetime.date(2017, 12, 12), "12-12-17")
    yield (datetime.date(2017, 1, 1), "1-1-17")
    yield (datetime.date(2016, 10, 11), "11-10-16")
    yield (datetime.date(2017, 1, 13), "13/1/17")
    yield (datetime.date(2017, 12, 12), "12/12/17")
    yield (datetime.date(2017, 1, 1), "1/1/17")
    yield (datetime.date(2016, 10, 11), "11/10/16")
    yield (datetime.date(2017, 1, 13), "13.1.17")
    yield (datetime.date(2017, 12, 12), "12.12.17")
    yield (datetime.date(2017, 1, 1), "1.1.17")
    yield (datetime.date(2016, 10, 11), "11.10.16")
    yield (datetime.date(2017, 1, 13), "13-1-17")
    yield (datetime.date(2017, 12, 12), "12-12-17")
    yield (datetime.date(2017, 1, 1), "01-1-17")
    yield (datetime.date(2016, 10, 11), "11-10-16")
    yield (datetime.date(2017, 1, 13), "13/1/17")
    yield (datetime.date(2017, 12, 12), "12/12/17")
    yield (datetime.date(2017, 1, 1), "01/1/17")
    yield (datetime.date(2016, 10, 11), "11/10/16")
    yield (datetime.date(2017, 1, 13), "13.1.17")
    yield (datetime.date(2017, 12, 12), "12.12.17")
    yield (datetime.date(2017, 1, 1), "01.1.17")
    yield (datetime.date(2016, 10, 11), "11.10.16")
    yield (datetime.date(2017, 1, 13), "13-01-17")
    yield (datetime.date(2017, 12, 12), "12-12-17")
    yield (datetime.date(2017, 1, 1), "1-01-17")
    yield (datetime.date(2016, 10, 11), "11-10-16")
    yield (datetime.date(2017, 1, 13), "13/01/17")
    yield (datetime.date(2017, 12, 12), "12/12/17")
    yield (datetime.date(2017, 1, 1), "1/01/17")
    yield (datetime.date(2016, 10, 11), "11/10/16")
    yield (datetime.date(2017, 1, 13), "13.01.17")
    yield (datetime.date(2017, 12, 12), "12.12.17")
    yield (datetime.date(2017, 1, 1), "1.01.17")
    yield (datetime.date(2016, 10, 11), "11.10.16")
    yield (datetime.date(2017, 1, 13), "13-01-17")
    yield (datetime.date(2017, 12, 12), "12-12-17")
    yield (datetime.date(2017, 1, 1), "01-01-17")
    yield (datetime.date(2016, 10, 11), "11-10-16")
    yield (datetime.date(2017, 1, 13), "13/01/17")
    yield (datetime.date(2017, 12, 12), "12/12/17")
    yield (datetime.date(2017, 1, 1), "01/01/17")
    yield (datetime.date(2016, 10, 11), "11/10/16")
    yield (datetime.date(2017, 1, 13), "13.01.17")
    yield (datetime.date(2017, 12, 12), "12.12.17")
    yield (datetime.date(2017, 1, 1), "01.01.17")
    yield (datetime.date(2016, 10, 11), "11.10.16")
    yield (datetime.date(2017, 1, 13), "13-Jan-17")
    yield (datetime.date(2017, 12, 12), "12-Dec-17")
    yield (datetime.date(2017, 1, 1), "1-Jan-17")
    yield (datetime.date(2016, 10, 11), "11-Oct-16")
    yield (datetime.date(2017, 1, 13), "13/Jan/17")
    yield (datetime.date(2017, 12, 12), "12/Dec/17")
    yield (datetime.date(2017, 1, 1), "1/Jan/17")
    yield (datetime.date(2016, 10, 11), "11/Oct/16")
    yield (datetime.date(2017, 1, 13), "13.Jan.17")
    yield (datetime.date(2017, 12, 12), "12.Dec.17")
    yield (datetime.date(2017, 1, 1), "1.Jan.17")
    yield (datetime.date(2016, 10, 11), "11.Oct.16")
    yield (datetime.date(2017, 1, 13), "13-Jan-17")
    yield (datetime.date(2017, 12, 12), "12-Dec-17")
    yield (datetime.date(2017, 1, 1), "01-Jan-17")
    yield (datetime.date(2016, 10, 11), "11-Oct-16")
    yield (datetime.date(2017, 1, 13), "13/Jan/17")
    yield (datetime.date(2017, 12, 12), "12/Dec/17")
    yield (datetime.date(2017, 1, 1), "01/Jan/17")
    yield (datetime.date(2016, 10, 11), "11/Oct/16")
    yield (datetime.date(2017, 1, 13), "13.Jan.17")
    yield (datetime.date(2017, 12, 12), "12.Dec.17")
    yield (datetime.date(2017, 1, 1), "01.Jan.17")
    yield (datetime.date(2016, 10, 11), "11.Oct.16")
    yield (datetime.date(2017, 1, 13), "13-1-2017")
    yield (datetime.date(2017, 12, 12), "12-12-2017")
    yield (datetime.date(2017, 1, 1), "1-1-2017")
    yield (datetime.date(2016, 10, 11), "11-10-2016")
    yield (datetime.date(2017, 1, 13), "13/1/2017")
    yield (datetime.date(2017, 12, 12), "12/12/2017")
    yield (datetime.date(2017, 1, 1), "1/1/2017")
    yield (datetime.date(2016, 10, 11), "11/10/2016")
    yield (datetime.date(2017, 1, 13), "13.1.2017")
    yield (datetime.date(2017, 12, 12), "12.12.2017")
    yield (datetime.date(2017, 1, 1), "1.1.2017")
    yield (datetime.date(2016, 10, 11), "11.10.2016")
    yield (datetime.date(2017, 1, 13), "13-1-2017")
    yield (datetime.date(2017, 12, 12), "12-12-2017")
    yield (datetime.date(2017, 1, 1), "01-1-2017")
    yield (datetime.date(2016, 10, 11), "11-10-2016")
    yield (datetime.date(2017, 1, 13), "13/1/2017")
    yield (datetime.date(2017, 12, 12), "12/12/2017")
    yield (datetime.date(2017, 1, 1), "01/1/2017")
    yield (datetime.date(2016, 10, 11), "11/10/2016")
    yield (datetime.date(2017, 1, 13), "13.1.2017")
    yield (datetime.date(2017, 12, 12), "12.12.2017")
    yield (datetime.date(2017, 1, 1), "01.1.2017")
    yield (datetime.date(2016, 10, 11), "11.10.2016")
    yield (datetime.date(2017, 1, 13), "13-01-2017")
    yield (datetime.date(2017, 12, 12), "12-12-2017")
    yield (datetime.date(2017, 1, 1), "1-01-2017")
    yield (datetime.date(2016, 10, 11), "11-10-2016")
    yield (datetime.date(2017, 1, 13), "13/01/2017")
    yield (datetime.date(2017, 12, 12), "12/12/2017")
    yield (datetime.date(2017, 1, 1), "1/01/2017")
    yield (datetime.date(2016, 10, 11), "11/10/2016")
    yield (datetime.date(2017, 1, 13), "13.01.2017")
    yield (datetime.date(2017, 12, 12), "12.12.2017")
    yield (datetime.date(2017, 1, 1), "1.01.2017")
    yield (datetime.date(2016, 10, 11), "11.10.2016")
    yield (datetime.date(2017, 1, 13), "13-01-2017")
    yield (datetime.date(2017, 12, 12), "12-12-2017")
    yield (datetime.date(2017, 1, 1), "01-01-2017")
    yield (datetime.date(2016, 10, 11), "11-10-2016")
    yield (datetime.date(2017, 1, 13), "13/01/2017")
    yield (datetime.date(2017, 12, 12), "12/12/2017")
    yield (datetime.date(2017, 1, 1), "01/01/2017")
    yield (datetime.date(2016, 10, 11), "11/10/2016")
    yield (datetime.date(2017, 1, 13), "13.01.2017")
    yield (datetime.date(2017, 12, 12), "12.12.2017")
    yield (datetime.date(2017, 1, 1), "01.01.2017")
    yield (datetime.date(2016, 10, 11), "11.10.2016")
    yield (datetime.date(2017, 1, 13), "13-Jan-2017")
    yield (datetime.date(2017, 12, 12), "12-Dec-2017")
    yield (datetime.date(2017, 1, 1), "1-Jan-2017")
    yield (datetime.date(2016, 10, 11), "11-Oct-2016")
    yield (datetime.date(2017, 1, 13), "13/Jan/2017")
    yield (datetime.date(2017, 12, 12), "12/Dec/2017")
    yield (datetime.date(2017, 1, 1), "1/Jan/2017")
    yield (datetime.date(2016, 10, 11), "11/Oct/2016")
    yield (datetime.date(2017, 1, 13), "13.Jan.2017")
    yield (datetime.date(2017, 12, 12), "12.Dec.2017")
    yield (datetime.date(2017, 1, 1), "1.Jan.2017")
    yield (datetime.date(2016, 10, 11), "11.Oct.2016")
    yield (datetime.date(2017, 1, 13), "13-Jan-2017")
    yield (datetime.date(2017, 12, 12), "12-Dec-2017")
    yield (datetime.date(2017, 1, 1), "01-Jan-2017")
    yield (datetime.date(2016, 10, 11), "11-Oct-2016")
    yield (datetime.date(2017, 1, 13), "13/Jan/2017")
    yield (datetime.date(2017, 12, 12), "12/Dec/2017")
    yield (datetime.date(2017, 1, 1), "01/Jan/2017")
    yield (datetime.date(2016, 10, 11), "11/Oct/2016")
    yield (datetime.date(2017, 1, 13), "13.Jan.2017")
    yield (datetime.date(2017, 12, 12), "12.Dec.2017")
    yield (datetime.date(2017, 1, 1), "01.Jan.2017")
    yield (datetime.date(2016, 10, 11), "11.Oct.2016")
    yield (datetime.date(2017, 1, 13), "1-13-17")
    yield (datetime.date(2017, 12, 12), "12-12-17")
    yield (datetime.date(2017, 1, 1), "1-1-17")
    yield (datetime.date(2016, 10, 11), "10-11-16")
    yield (datetime.date(2017, 1, 13), "1/13/17")
    yield (datetime.date(2017, 12, 12), "12/12/17")
    yield (datetime.date(2017, 1, 1), "1/1/17")
    yield (datetime.date(2016, 10, 11), "10/11/16")
    yield (datetime.date(2017, 1, 13), "1.13.17")
    yield (datetime.date(2017, 12, 12), "12.12.17")
    yield (datetime.date(2017, 1, 1), "1.1.17")
    yield (datetime.date(2016, 10, 11), "10.11.16")
    yield (datetime.date(2017, 1, 13), "1-13-17")
    yield (datetime.date(2017, 12, 12), "12-12-17")
    yield (datetime.date(2017, 1, 1), "1-01-17")
    yield (datetime.date(2016, 10, 11), "10-11-16")
    yield (datetime.date(2017, 1, 13), "1/13/17")
    yield (datetime.date(2017, 12, 12), "12/12/17")
    yield (datetime.date(2017, 1, 1), "1/01/17")
    yield (datetime.date(2016, 10, 11), "10/11/16")
    yield (datetime.date(2017, 1, 13), "1.13.17")
    yield (datetime.date(2017, 12, 12), "12.12.17")
    yield (datetime.date(2017, 1, 1), "1.01.17")
    yield (datetime.date(2016, 10, 11), "10.11.16")
    yield (datetime.date(2017, 1, 13), "01-13-17")
    yield (datetime.date(2017, 12, 12), "12-12-17")
    yield (datetime.date(2017, 1, 1), "01-1-17")
    yield (datetime.date(2016, 10, 11), "10-11-16")
    yield (datetime.date(2017, 1, 13), "01/13/17")
    yield (datetime.date(2017, 12, 12), "12/12/17")
    yield (datetime.date(2017, 1, 1), "01/1/17")
    yield (datetime.date(2016, 10, 11), "10/11/16")
    yield (datetime.date(2017, 1, 13), "01.13.17")
    yield (datetime.date(2017, 12, 12), "12.12.17")
    yield (datetime.date(2017, 1, 1), "01.1.17")
    yield (datetime.date(2016, 10, 11), "10.11.16")
    yield (datetime.date(2017, 1, 13), "01-13-17")
    yield (datetime.date(2017, 12, 12), "12-12-17")
    yield (datetime.date(2017, 1, 1), "01-01-17")
    yield (datetime.date(2016, 10, 11), "10-11-16")
    yield (datetime.date(2017, 1, 13), "01/13/17")
    yield (datetime.date(2017, 12, 12), "12/12/17")
    yield (datetime.date(2017, 1, 1), "01/01/17")
    yield (datetime.date(2016, 10, 11), "10/11/16")
    yield (datetime.date(2017, 1, 13), "01.13.17")
    yield (datetime.date(2017, 12, 12), "12.12.17")
    yield (datetime.date(2017, 1, 1), "01.01.17")
    yield (datetime.date(2016, 10, 11), "10.11.16")
    yield (datetime.date(2017, 1, 13), "Jan-13-17")
    yield (datetime.date(2017, 12, 12), "Dec-12-17")
    yield (datetime.date(2017, 1, 1), "Jan-1-17")
    yield (datetime.date(2016, 10, 11), "Oct-11-16")
    yield (datetime.date(2017, 1, 13), "Jan/13/17")
    yield (datetime.date(2017, 12, 12), "Dec/12/17")
    yield (datetime.date(2017, 1, 1), "Jan/1/17")
    yield (datetime.date(2016, 10, 11), "Oct/11/16")
    yield (datetime.date(2017, 1, 13), "Jan.13.17")
    yield (datetime.date(2017, 12, 12), "Dec.12.17")
    yield (datetime.date(2017, 1, 1), "Jan.1.17")
    yield (datetime.date(2016, 10, 11), "Oct.11.16")
    yield (datetime.date(2017, 1, 13), "Jan-13-17")
    yield (datetime.date(2017, 12, 12), "Dec-12-17")
    yield (datetime.date(2017, 1, 1), "Jan-01-17")
    yield (datetime.date(2016, 10, 11), "Oct-11-16")
    yield (datetime.date(2017, 1, 13), "Jan/13/17")
    yield (datetime.date(2017, 12, 12), "Dec/12/17")
    yield (datetime.date(2017, 1, 1), "Jan/01/17")
    yield (datetime.date(2016, 10, 11), "Oct/11/16")
    yield (datetime.date(2017, 1, 13), "Jan.13.17")
    yield (datetime.date(2017, 12, 12), "Dec.12.17")
    yield (datetime.date(2017, 1, 1), "Jan.01.17")
    yield (datetime.date(2016, 10, 11), "Oct.11.16")
    yield (datetime.date(2017, 1, 13), "1-13-2017")
    yield (datetime.date(2017, 12, 12), "12-12-2017")
    yield (datetime.date(2017, 1, 1), "1-1-2017")
    yield (datetime.date(2016, 10, 11), "10-11-2016")
    yield (datetime.date(2017, 1, 13), "1/13/2017")
    yield (datetime.date(2017, 12, 12), "12/12/2017")
    yield (datetime.date(2017, 1, 1), "1/1/2017")
    yield (datetime.date(2016, 10, 11), "10/11/2016")
    yield (datetime.date(2017, 1, 13), "1.13.2017")
    yield (datetime.date(2017, 12, 12), "12.12.2017")
    yield (datetime.date(2017, 1, 1), "1.1.2017")
    yield (datetime.date(2016, 10, 11), "10.11.2016")
    yield (datetime.date(2017, 1, 13), "1-13-2017")
    yield (datetime.date(2017, 12, 12), "12-12-2017")
    yield (datetime.date(2017, 1, 1), "1-01-2017")
    yield (datetime.date(2016, 10, 11), "10-11-2016")
    yield (datetime.date(2017, 1, 13), "1/13/2017")
    yield (datetime.date(2017, 12, 12), "12/12/2017")
    yield (datetime.date(2017, 1, 1), "1/01/2017")
    yield (datetime.date(2016, 10, 11), "10/11/2016")
    yield (datetime.date(2017, 1, 13), "1.13.2017")
    yield (datetime.date(2017, 12, 12), "12.12.2017")
    yield (datetime.date(2017, 1, 1), "1.01.2017")
    yield (datetime.date(2016, 10, 11), "10.11.2016")
    yield (datetime.date(2017, 1, 13), "01-13-2017")
    yield (datetime.date(2017, 12, 12), "12-12-2017")
    yield (datetime.date(2017, 1, 1), "01-1-2017")
    yield (datetime.date(2016, 10, 11), "10-11-2016")
    yield (datetime.date(2017, 1, 13), "01/13/2017")
    yield (datetime.date(2017, 12, 12), "12/12/2017")
    yield (datetime.date(2017, 1, 1), "01/1/2017")
    yield (datetime.date(2016, 10, 11), "10/11/2016")
    yield (datetime.date(2017, 1, 13), "01.13.2017")
    yield (datetime.date(2017, 12, 12), "12.12.2017")
    yield (datetime.date(2017, 1, 1), "01.1.2017")
    yield (datetime.date(2016, 10, 11), "10.11.2016")
    yield (datetime.date(2017, 1, 13), "01-13-2017")
    yield (datetime.date(2017, 12, 12), "12-12-2017")
    yield (datetime.date(2017, 1, 1), "01-01-2017")
    yield (datetime.date(2016, 10, 11), "10-11-2016")
    yield (datetime.date(2017, 1, 13), "01/13/2017")
    yield (datetime.date(2017, 12, 12), "12/12/2017")
    yield (datetime.date(2017, 1, 1), "01/01/2017")
    yield (datetime.date(2016, 10, 11), "10/11/2016")
    yield (datetime.date(2017, 1, 13), "01.13.2017")
    yield (datetime.date(2017, 12, 12), "12.12.2017")
    yield (datetime.date(2017, 1, 1), "01.01.2017")
    yield (datetime.date(2016, 10, 11), "10.11.2016")
    yield (datetime.date(2017, 1, 13), "Jan-13-2017")
    yield (datetime.date(2017, 12, 12), "Dec-12-2017")
    yield (datetime.date(2017, 1, 1), "Jan-1-2017")
    yield (datetime.date(2016, 10, 11), "Oct-11-2016")
    yield (datetime.date(2017, 1, 13), "Jan/13/2017")
    yield (datetime.date(2017, 12, 12), "Dec/12/2017")
    yield (datetime.date(2017, 1, 1), "Jan/1/2017")
    yield (datetime.date(2016, 10, 11), "Oct/11/2016")
    yield (datetime.date(2017, 1, 13), "Jan.13.2017")
    yield (datetime.date(2017, 12, 12), "Dec.12.2017")
    yield (datetime.date(2017, 1, 1), "Jan.1.2017")
    yield (datetime.date(2016, 10, 11), "Oct.11.2016")
    yield (datetime.date(2017, 1, 13), "Jan-13-2017")
    yield (datetime.date(2017, 12, 12), "Dec-12-2017")
    yield (datetime.date(2017, 1, 1), "Jan-01-2017")
    yield (datetime.date(2016, 10, 11), "Oct-11-2016")
    yield (datetime.date(2017, 1, 13), "Jan/13/2017")
    yield (datetime.date(2017, 12, 12), "Dec/12/2017")
    yield (datetime.date(2017, 1, 1), "Jan/01/2017")
    yield (datetime.date(2016, 10, 11), "Oct/11/2016")
    yield (datetime.date(2017, 1, 13), "Jan.13.2017")
    yield (datetime.date(2017, 12, 12), "Dec.12.2017")
    yield (datetime.date(2017, 1, 1), "Jan.01.2017")
    yield (datetime.date(2016, 10, 11), "Oct.11.2016")
    yield (datetime.date(2017, 1, 13), "17-1-13")
    yield (datetime.date(2017, 12, 12), "17-12-12")
    yield (datetime.date(2017, 1, 1), "17-1-1")
    yield (datetime.date(2016, 10, 11), "16-10-11")
    yield (datetime.date(2017, 1, 13), "17/1/13")
    yield (datetime.date(2017, 12, 12), "17/12/12")
    yield (datetime.date(2017, 1, 1), "17/1/1")
    yield (datetime.date(2016, 10, 11), "16/10/11")
    yield (datetime.date(2017, 1, 13), "17.1.13")
    yield (datetime.date(2017, 12, 12), "17.12.12")
    yield (datetime.date(2017, 1, 1), "17.1.1")
    yield (datetime.date(2016, 10, 11), "16.10.11")
    yield (datetime.date(2017, 1, 13), "17-1-13")
    yield (datetime.date(2017, 12, 12), "17-12-12")
    yield (datetime.date(2017, 1, 1), "17-1-01")
    yield (datetime.date(2016, 10, 11), "16-10-11")
    yield (datetime.date(2017, 1, 13), "17/1/13")
    yield (datetime.date(2017, 12, 12), "17/12/12")
    yield (datetime.date(2017, 1, 1), "17/1/01")
    yield (datetime.date(2016, 10, 11), "16/10/11")
    yield (datetime.date(2017, 1, 13), "17.1.13")
    yield (datetime.date(2017, 12, 12), "17.12.12")
    yield (datetime.date(2017, 1, 1), "17.1.01")
    yield (datetime.date(2016, 10, 11), "16.10.11")
    yield (datetime.date(2017, 1, 13), "17-01-13")
    yield (datetime.date(2017, 12, 12), "17-12-12")
    yield (datetime.date(2017, 1, 1), "17-01-1")
    yield (datetime.date(2016, 10, 11), "16-10-11")
    yield (datetime.date(2017, 1, 13), "17/01/13")
    yield (datetime.date(2017, 12, 12), "17/12/12")
    yield (datetime.date(2017, 1, 1), "17/01/1")
    yield (datetime.date(2016, 10, 11), "16/10/11")
    yield (datetime.date(2017, 1, 13), "17.01.13")
    yield (datetime.date(2017, 12, 12), "17.12.12")
    yield (datetime.date(2017, 1, 1), "17.01.1")
    yield (datetime.date(2016, 10, 11), "16.10.11")
    yield (datetime.date(2017, 1, 13), "17-01-13")
    yield (datetime.date(2017, 12, 12), "17-12-12")
    yield (datetime.date(2017, 1, 1), "17-01-01")
    yield (datetime.date(2016, 10, 11), "16-10-11")
    yield (datetime.date(2017, 1, 13), "17/01/13")
    yield (datetime.date(2017, 12, 12), "17/12/12")
    yield (datetime.date(2017, 1, 1), "17/01/01")
    yield (datetime.date(2016, 10, 11), "16/10/11")
    yield (datetime.date(2017, 1, 13), "17.01.13")
    yield (datetime.date(2017, 12, 12), "17.12.12")
    yield (datetime.date(2017, 1, 1), "17.01.01")
    yield (datetime.date(2016, 10, 11), "16.10.11")
    yield (datetime.date(2017, 1, 13), "17-Jan-13")
    yield (datetime.date(2017, 12, 12), "17-Dec-12")
    yield (datetime.date(2017, 1, 1), "17-Jan-1")
    yield (datetime.date(2016, 10, 11), "16-Oct-11")
    yield (datetime.date(2017, 1, 13), "17/Jan/13")
    yield (datetime.date(2017, 12, 12), "17/Dec/12")
    yield (datetime.date(2017, 1, 1), "17/Jan/1")
    yield (datetime.date(2016, 10, 11), "16/Oct/11")
    yield (datetime.date(2017, 1, 13), "17.Jan.13")
    yield (datetime.date(2017, 12, 12), "17.Dec.12")
    yield (datetime.date(2017, 1, 1), "17.Jan.1")
    yield (datetime.date(2016, 10, 11), "16.Oct.11")
    yield (datetime.date(2017, 1, 13), "17-Jan-13")
    yield (datetime.date(2017, 12, 12), "17-Dec-12")
    yield (datetime.date(2017, 1, 1), "17-Jan-01")
    yield (datetime.date(2016, 10, 11), "16-Oct-11")
    yield (datetime.date(2017, 1, 13), "17/Jan/13")
    yield (datetime.date(2017, 12, 12), "17/Dec/12")
    yield (datetime.date(2017, 1, 1), "17/Jan/01")
    yield (datetime.date(2016, 10, 11), "16/Oct/11")
    yield (datetime.date(2017, 1, 13), "17.Jan.13")
    yield (datetime.date(2017, 12, 12), "17.Dec.12")
    yield (datetime.date(2017, 1, 1), "17.Jan.01")
    yield (datetime.date(2016, 10, 11), "16.Oct.11")
    yield (datetime.date(2017, 1, 13), "2017-1-13")
    yield (datetime.date(2017, 12, 12), "2017-12-12")
    yield (datetime.date(2017, 1, 1), "2017-1-1")
    yield (datetime.date(2016, 10, 11), "2016-10-11")
    yield (datetime.date(2017, 1, 13), "2017/1/13")
    yield (datetime.date(2017, 12, 12), "2017/12/12")
    yield (datetime.date(2017, 1, 1), "2017/1/1")
    yield (datetime.date(2016, 10, 11), "2016/10/11")
    yield (datetime.date(2017, 1, 13), "2017.1.13")
    yield (datetime.date(2017, 12, 12), "2017.12.12")
    yield (datetime.date(2017, 1, 1), "2017.1.1")
    yield (datetime.date(2016, 10, 11), "2016.10.11")
    yield (datetime.date(2017, 1, 13), "2017-1-13")
    yield (datetime.date(2017, 12, 12), "2017-12-12")
    yield (datetime.date(2017, 1, 1), "2017-1-01")
    yield (datetime.date(2016, 10, 11), "2016-10-11")
    yield (datetime.date(2017, 1, 13), "2017/1/13")
    yield (datetime.date(2017, 12, 12), "2017/12/12")
    yield (datetime.date(2017, 1, 1), "2017/1/01")
    yield (datetime.date(2016, 10, 11), "2016/10/11")
    yield (datetime.date(2017, 1, 13), "2017.1.13")
    yield (datetime.date(2017, 12, 12), "2017.12.12")
    yield (datetime.date(2017, 1, 1), "2017.1.01")
    yield (datetime.date(2016, 10, 11), "2016.10.11")
    yield (datetime.date(2017, 1, 13), "2017-01-13")
    yield (datetime.date(2017, 12, 12), "2017-12-12")
    yield (datetime.date(2017, 1, 1), "2017-01-1")
    yield (datetime.date(2016, 10, 11), "2016-10-11")
    yield (datetime.date(2017, 1, 13), "2017/01/13")
    yield (datetime.date(2017, 12, 12), "2017/12/12")
    yield (datetime.date(2017, 1, 1), "2017/01/1")
    yield (datetime.date(2016, 10, 11), "2016/10/11")
    yield (datetime.date(2017, 1, 13), "2017.01.13")
    yield (datetime.date(2017, 12, 12), "2017.12.12")
    yield (datetime.date(2017, 1, 1), "2017.01.1")
    yield (datetime.date(2016, 10, 11), "2016.10.11")
    yield (datetime.date(2017, 1, 13), "2017-01-13")
    yield (datetime.date(2017, 12, 12), "2017-12-12")
    yield (datetime.date(2017, 1, 1), "2017-01-01")
    yield (datetime.date(2016, 10, 11), "2016-10-11")
    yield (datetime.date(2017, 1, 13), "2017/01/13")
    yield (datetime.date(2017, 12, 12), "2017/12/12")
    yield (datetime.date(2017, 1, 1), "2017/01/01")
    yield (datetime.date(2016, 10, 11), "2016/10/11")
    yield (datetime.date(2017, 1, 13), "2017.01.13")
    yield (datetime.date(2017, 12, 12), "2017.12.12")
    yield (datetime.date(2017, 1, 1), "2017.01.01")
    yield (datetime.date(2016, 10, 11), "2016.10.11")
    yield (datetime.date(2017, 1, 13), "2017-Jan-13")
    yield (datetime.date(2017, 12, 12), "2017-Dec-12")
    yield (datetime.date(2017, 1, 1), "2017-Jan-1")
    yield (datetime.date(2016, 10, 11), "2016-Oct-11")
    yield (datetime.date(2017, 1, 13), "2017/Jan/13")
    yield (datetime.date(2017, 12, 12), "2017/Dec/12")
    yield (datetime.date(2017, 1, 1), "2017/Jan/1")
    yield (datetime.date(2016, 10, 11), "2016/Oct/11")
    yield (datetime.date(2017, 1, 13), "2017.Jan.13")
    yield (datetime.date(2017, 12, 12), "2017.Dec.12")
    yield (datetime.date(2017, 1, 1), "2017.Jan.1")
    yield (datetime.date(2016, 10, 11), "2016.Oct.11")
    yield (datetime.date(2017, 1, 13), "2017-Jan-13")
    yield (datetime.date(2017, 12, 12), "2017-Dec-12")
    yield (datetime.date(2017, 1, 1), "2017-Jan-01")
    yield (datetime.date(2016, 10, 11), "2016-Oct-11")
    yield (datetime.date(2017, 1, 13), "2017/Jan/13")
    yield (datetime.date(2017, 12, 12), "2017/Dec/12")
    yield (datetime.date(2017, 1, 1), "2017/Jan/01")
    yield (datetime.date(2016, 10, 11), "2016/Oct/11")
    yield (datetime.date(2017, 1, 13), "2017.Jan.13")
    yield (datetime.date(2017, 12, 12), "2017.Dec.12")
    yield (datetime.date(2017, 1, 1), "2017.Jan.01")
    yield (datetime.date(2016, 10, 11), "2016.Oct.11")

if __name__ == '__main__':
    import doctest
    doctest.testmod()
