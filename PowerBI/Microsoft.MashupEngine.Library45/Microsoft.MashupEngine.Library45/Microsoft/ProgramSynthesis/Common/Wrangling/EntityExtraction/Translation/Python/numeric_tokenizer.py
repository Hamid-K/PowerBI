class NumericToken(EntityToken):
    __slots__ = ('number_value',)

    class_id = EntityType.Number

    def __init__(self, source, start, end, number_value):
        super().__init__(source, start, end)
        self.number_value = number_value

    def __eq__(self, other):
        if other is None:
            return False
        if other is self:
            return True
        return type(self) == type(other) and self.number_value == other.number_value

    def __hash__(self):
        return hash(str(type(self))) ^ hash(self.number_value)

    @staticmethod
    def create(source, start, end, v, is_formatted):
        return FormattedNumberToken(source, start, end, v) if is_formatted else NumericToken(source, start, end, v)


class FormattedNumberToken(NumericToken):
    class_id = EntityType.Unknown


class HexNumberToken(NumericToken):
    class_id = EntityType.HexadecimalNumber


class NumericTokenizer(RegexBasedTokenizer):
    """
    >>> t = NumericTokenizer()
    >>> data = "noise 1234 more noise"
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> isinstance(tokens[0], NumericToken)
    True
    >>> tokens[0].number_value
    1234.0
    >>> data = "noise 1234.567 and more noise"
    >>> tokens = list(t.match_tokens(data))
    >>> filtered = list(filter(lambda tok: type(tok) == NumericToken, tokens))
    >>> len(filtered)
    1
    >>> filtered[0].number_value
    1234.567
    >>> data = "noise 1234.567E+12 more noise"
    >>> tokens = list(t.match_tokens(data))
    >>> filtered = list(filter(lambda tok: type(tok) == NumericToken, tokens))
    >>> len(filtered)
    1
    >>> filtered[0].number_value
    1234567000000000.0
    >>> data = "noise +12345.675 and yet more noise"
    >>> tokens = list(t.match_tokens(data))
    >>> filtered = list(filter(lambda tok: type(tok) == NumericToken, tokens))
    >>> len(filtered)
    1
    >>> filtered[0].number_value
    12345.675
    >>> data = "noise 0x12345678 sputters and pops"
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    2
    >>> str(tokens[0])
    '0x12345678'
    """
    full_number_match_group_name = "FullNumberGroup"
    first_digit_group_name = "FirstDigitGroup"
    second_digit_group_name = "SecondDigitGroup"
    third_digit_group_name = "ThirdDigitGroup"
    hex_number_group_name = "HexNumberGroup"

    separated_number_pattern_format_string = \
        r"(?<{0}>(?<{1}>\d+)(?:{{0}}(?<{2}>\d{{{{2,3}}}}))+(?:{{1}}(?<{3}>\d+))?)".format(
            full_number_match_group_name,
            first_digit_group_name,
            second_digit_group_name,
            third_digit_group_name
        )

    scientific_number_pattern_string = r"(?<{0}>(?:\-|\+)?\d+(?:\.\d+(?:[eE][\+\-]\d+)?)?)".format(
        full_number_match_group_name,
    )

    hex_left_context_pattern_string = r"(?:^|[^\dxX0-9a-fA-F])"
    hex_right_context_pattern_string = r"(?:$|[^\dxX0-9a-fA-F])"
    hex_number_pattern_string = r"0[xX](?<{0}>[0-9a-fA-F])+".format(hex_number_group_name)

    @staticmethod
    def get_all_separated_pattern_strings():
        thousands_separators = [',', '\\.', ' ', '\'']
        decimal_separators = ['\\.', ',']
        all_separator_pairs = filter(lambda tup: tup[0] != tup[1],
                                     itertools.product(thousands_separators, decimal_separators))
        all_separated_pattern_strings = [NumericTokenizer.separated_number_pattern_format_string.format(x[0], x[1])
                                         for x in all_separator_pairs]
        return all_separated_pattern_strings

    def __init__(self):
        self.scientific_number_token_pattern = TokenPattern(NumericTokenizer.scientific_number_pattern_string)
        self.hex_number_token_pattern = TokenPattern(NumericTokenizer.hex_number_pattern_string,
                                                     NumericTokenizer.hex_left_context_pattern_string,
                                                     NumericTokenizer.hex_right_context_pattern_string)

        super().__init__(OverlapStrategy.SUBSUMPTION,
                         self.scientific_number_token_pattern,
                         self.hex_number_token_pattern,
                         *[TokenPattern(x) for x in NumericTokenizer.get_all_separated_pattern_strings()])

    def process_matches(self, matches):
        for m in matches:
            val = m.value()
            if val.lower().startswith('0x'):
                long_value = int(val, 0)
                yield NumericToken(m.source, m.start, m.end, long_value)
                yield HexNumberToken(m.source, m.start, m.end, long_value)
                return

            is_formatted = (m.token_pattern is not self.scientific_number_token_pattern and
                            m.token_pattern is not self.hex_number_token_pattern)
            r = NumericTokenizer.try_parse_number_match(m.full_match)
            if r is not None:
                yield NumericToken.create(m.source, m.start, m.end, r, is_formatted)

    @staticmethod
    def try_parse_number_match(match):
        s = match.string
        r = RegexBasedTokenizer.try_parse_float(s[match.start():match.end()])
        if r is not None:
            return r
        full_match_span = RegexBasedTokenizer.get_group_span(match, NumericTokenizer.full_number_match_group_name)
        if full_match_span is not None:
            r = RegexBasedTokenizer.try_parse_float(s[full_match_span[0]:full_match_span[1]])
            if r is not None:
                return r

        first_span = RegexBasedTokenizer.get_group_span(match, NumericTokenizer.first_digit_group_name)
        second_spans = RegexBasedTokenizer.get_group_spans(match, NumericTokenizer.second_digit_group_name)
        third_span = RegexBasedTokenizer.get_group_span(match, NumericTokenizer.third_digit_group_name)

        if first_span is None:
            return None

        first_number_str = s[first_span[0]:first_span[1]]
        second_number_str = ''.join(s[x[0]:x[1]] for x in second_spans)
        third_number_str = s[third_span[0]:third_span[1]]

        return RegexBasedTokenizer.try_parse_float(first_number_str + second_number_str + third_number_str)


if __name__ == '__main__':
    import doctest
    doctest.testmod()
