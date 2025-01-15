class PhoneNumberToken(EntityToken):
    class_id = EntityType.PhoneNumber

    def __eq__(self, other):
        return self._value_based_equality(other)

    def __hash__(self):
        return self._value_based_hash()


class PhoneNumberTokenizer(RegexBasedTokenizer):
    """
    >>> t = PhoneNumberTokenizer()
    >>> all_results = []
    >>> for test_case in _test_cases():
    ...     data = "foo " + test_case + " bar"
    ...     tokens = t.match_tokens(data)
    ...     phone_tokens = list(filter(lambda x: type(x) == PhoneNumberToken, tokens))
    ...     all_results.append(len(phone_tokens) == 1 and str(phone_tokens[0]) == test_case)
    >>> all(x for x in all_results)
    True
    """
    min_digits = 7
    max_digits = 10
    min_digits_with_cc = 11
    max_digits_with_cc = 13

    left_context_pattern_string = r"(?:^|[^\d\-\.])"
    right_context_pattern_string = r"(?:$|[^\d\-\.])"
    left_context_pattern_string_with_cc = r"(?:^|[^\d\-\.\+])"
    separator_pattern_string = r"[\-\.\s]"
    country_code_pattern_string = r"(?:\+\d{1,3})"

    phone_number_pattern_string_1 = r"\(\d{{3}}\){0}?\d{{3}}{1}?\d{{4}}".format(
        separator_pattern_string,
        separator_pattern_string
    )

    phone_number_pattern_string_1_with_cc = r"{0}{1}?\(\d{{3}}\){2}?\d{{3}}{3}?\d{{4}}".format(
            country_code_pattern_string,
            separator_pattern_string,
            separator_pattern_string,
            separator_pattern_string
    )

    phone_number_pattern_string_2 = r"\d{{3}}{0}?\d{{3}}{0}?\d{{4}}".format(separator_pattern_string)
    phone_number_pattern_string_2_with_cc = r"{0}{1}?\d{{3}}{1}?\d{{3}}{1}?\d{{4}}".format(
        country_code_pattern_string,
        separator_pattern_string,
    )
    phone_number_pattern_string_3 = r"\d{{3}}{0}?\d{{4}}".format(separator_pattern_string)
    phone_number_pattern_string_4 = r"\d+(?:\d+{0}?)*\d+".format(separator_pattern_string)
    phone_number_pattern_string_4_with_cc = r"\+\d{{1,3}}(?:\d+{0}?)*\d+".format(separator_pattern_string)

    date_separator_regex = regex.compile(r"[\-\.\s\p{Pd}]")

    def __init__(self):
        super().__init__(OverlapStrategy.SUBSUMPTION,
                         TokenPattern(PhoneNumberTokenizer.phone_number_pattern_string_1,
                                      PhoneNumberTokenizer.left_context_pattern_string,
                                      PhoneNumberTokenizer.right_context_pattern_string),
                         TokenPattern(PhoneNumberTokenizer.phone_number_pattern_string_1_with_cc,
                                      PhoneNumberTokenizer.left_context_pattern_string_with_cc,
                                      PhoneNumberTokenizer.right_context_pattern_string),
                         TokenPattern(PhoneNumberTokenizer.phone_number_pattern_string_2,
                                      PhoneNumberTokenizer.left_context_pattern_string,
                                      PhoneNumberTokenizer.right_context_pattern_string),
                         TokenPattern(PhoneNumberTokenizer.phone_number_pattern_string_2_with_cc,
                                      PhoneNumberTokenizer.left_context_pattern_string_with_cc,
                                      PhoneNumberTokenizer.right_context_pattern_string),
                         TokenPattern(PhoneNumberTokenizer.phone_number_pattern_string_3,
                                      PhoneNumberTokenizer.left_context_pattern_string,
                                      PhoneNumberTokenizer.right_context_pattern_string),
                         TokenPattern(PhoneNumberTokenizer.phone_number_pattern_string_4,
                                      PhoneNumberTokenizer.left_context_pattern_string,
                                      PhoneNumberTokenizer.right_context_pattern_string),
                         TokenPattern(PhoneNumberTokenizer.phone_number_pattern_string_4_with_cc,
                                      PhoneNumberTokenizer.left_context_pattern_string_with_cc,
                                      PhoneNumberTokenizer.right_context_pattern_string))

    def process_matches(self, matches):
        for m in matches:
            actual_match = m.content_group[0]
            digit_count = len(list(filter(lambda c: c.isdigit(), actual_match)))
            dot_count = len(list(filter(lambda c: c == '.', actual_match)))
            separator_matches = PhoneNumberTokenizer.date_separator_regex.findall(actual_match)
            separator_count = len(separator_matches)
            distinct_separator_count = len(set(separator_matches))
            non_digit_count = len(actual_match) - digit_count

            if dot_count == 1 and non_digit_count == separator_count:
                # this is a number
                continue

            if distinct_separator_count > 1 and non_digit_count == separator_count:
                # digits separated by non-uniform separators. Unlikely to be a number
                continue

            has_country_code = actual_match.startswith('+')
            if not has_country_code:
                if digit_count != PhoneNumberTokenizer.min_digits and digit_count != PhoneNumberTokenizer.max_digits:
                    continue
                yield PhoneNumberToken(m.source, m.start, m.end)
                continue

            if PhoneNumberTokenizer.min_digits_with_cc <= digit_count <= PhoneNumberTokenizer.max_digits_with_cc:
                yield PhoneNumberToken(m.source, m.start, m.end)


# testing code
def _test_cases():
    yield r"(123)4567890"
    yield r"(123)-4567890"
    yield r"(123)-456-7890"
    yield r"(123)456-7890"
    yield r"(123).456-7890"
    yield r"(123)456.7890"
    yield r"(123)-456.7890"
    yield r"(123).456.7890"
    yield r"(123).4567890"
    yield r"123-456-7890"
    yield r"123.456.7890"
    yield r"123456-7890"
    yield r"123-4567890"
    yield r"1234567890"
    yield r"123 456 7890"
    yield r"+1 123 456 7890"
    yield r"+1.123.456.7890"
    yield r"+1.123-456-7890"
    yield r"+1-123-456-7890"
    yield r"+1 123-456-7890"
    yield r"+11234567890"
    yield r"+91 9876512345"
    yield r"+91-2345678901"
    yield r"345-1234"
