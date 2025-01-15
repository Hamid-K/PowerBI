class CurrencyToken(NumericToken):
    __slots__ = ('currency_symbol',)

    class_id = EntityType.Currency

    def __init__(self, source, start, end, number_value, currency_symbol):
        super().__init__(source, start, end, number_value)
        self.currency_symbol = currency_symbol

    def __eq__(self, other):
        return self._value_based_equality(other)

    def __hash__(self):
        return self._value_based_hash()


class CurrencyTokenizer(RegexBasedTokenizer):
    """
    >>> t = CurrencyTokenizer()
    >>> data = r"$1000 00,42"
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> isinstance(tokens[0], CurrencyToken)
    True
    >>> tokens[0].currency_symbol
    '$'
    >>> str(tokens[0])
    '$1000 00,42'
    >>> data = r"1000 00,42$"
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> isinstance(tokens[0], CurrencyToken)
    True
    >>> tokens[0].currency_symbol
    '$'
    >>> str(tokens[0])
    '1000 00,42$'
    >>> data = r"$1000,000.42"
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> isinstance(tokens[0], CurrencyToken)
    True
    >>> tokens[0].currency_symbol
    '$'
    >>> str(tokens[0])
    '$1000,000.42'
    >>> data = r"1000,000.42$"
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> isinstance(tokens[0], CurrencyToken)
    True
    >>> tokens[0].currency_symbol
    '$'
    >>> str(tokens[0])
    '1000,000.42$'
    >>> data = r"¥1000,000.42"
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> isinstance(tokens[0], CurrencyToken)
    True
    >>> tokens[0].currency_symbol
    '¥'
    >>> str(tokens[0])
    '¥1000,000.42'
    >>> data = r"1000,000.42¥"
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> isinstance(tokens[0], CurrencyToken)
    True
    >>> tokens[0].currency_symbol
    '¥'
    >>> str(tokens[0])
    '1000,000.42¥'
    """

    currency_group_name = "CurrencySymbol"
    prefix_currency_left_context_pattern_string = r"(?:^|[^\p{Sc}])"
    prefix_currency_right_context_pattern_string = r"(?:$|[^\d])"
    suffix_currency_left_context_pattern_string = r"(?:^|[^\d])"
    suffix_currency_right_context_pattern_string = r"(?:$|[^\p{Sc}])"

    currency_pattern_string = r"(?<{0}>\p{{Sc}})".format(currency_group_name)

    def __init__(self):
        patterns = []
        for s in NumericTokenizer.get_all_separated_pattern_strings():
            patterns.append(TokenPattern(CurrencyTokenizer.currency_pattern_string + '\\s*' + s,
                                         CurrencyTokenizer.prefix_currency_left_context_pattern_string,
                                         CurrencyTokenizer.prefix_currency_right_context_pattern_string))
            patterns.append(TokenPattern(s + '\\s*' + CurrencyTokenizer.currency_pattern_string,
                                         CurrencyTokenizer.suffix_currency_left_context_pattern_string,
                                         CurrencyTokenizer.suffix_currency_right_context_pattern_string))
            super().__init__(OverlapStrategy.SUBSUMPTION, *patterns)

    def process_matches(self, matches):
        for m in matches:
            r = NumericTokenizer.try_parse_number_match(m.full_match)
            if r is None:
                continue
            currency_span = RegexBasedTokenizer.get_group_span(m.full_match, CurrencyTokenizer.currency_group_name)
            s = m.source
            yield CurrencyToken(m.source, m.start, m.end, r, s[currency_span[0]:currency_span[1]])


if __name__ == '__main__':
    import doctest
    doctest.testmod()
