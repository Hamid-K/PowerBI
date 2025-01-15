class UrlToken(EntityToken):
    class_id = EntityType.Url

    def __eq__(self, other):
        return self._value_based_equality(other)

    def __hash__(self):
        return self._value_based_hash()


class UrlTokenizer(RegexBasedTokenizer):
    """
    >>> t = UrlTokenizer()
    >>> test_url = r'''http://stackoverflow.com/questions/2708178/python-using-doctests-for-classes'''
    >>> tokens = list(t.match_tokens(test_url))
    >>> len(tokens)
    1
    >>> str(tokens[0])
    'http://stackoverflow.com/questions/2708178/python-using-doctests-for-classes'
    """

    url_pattern_string = r'''(?:(?:https?|ftp)://)(?:\S+(?::\S*)?@)?(:?(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])''' + \
        r'''(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))''' + \
        r'''|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)''' + \
        r'''(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*''' + \
        r'''(?:\.(?:[a-z\u00a1-\uffff]{2,}))\.?)(?::\d{2,5})?(?:[/?#]\S*)?'''

    def __init__(self):
        super().__init__(OverlapStrategy.NONE,
                         TokenPattern(UrlTokenizer.url_pattern_string,
                                      None, None, False, False, regex.IGNORECASE))

    def process_matches(self, matches):
        return super()._default_process_matches_impl(UrlToken, matches)
