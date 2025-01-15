class EmailToken(EntityToken):
    __slots__ = ('user_name', 'domain_name')

    class_id = EntityType.EmailAddress

    def __init__(self, source, start, end, user_name, domain_name):
        super().__init__(source, start, end)
        self.user_name = user_name
        self.domain_name = domain_name

    def __eq__(self, other):
        return self._value_based_equality(other)

    def __hash__(self):
        return self._value_based_hash()


class EmailTokenizer(RegexBasedTokenizer):
    """
    >>> t = EmailTokenizer()
    >>> data = 'abc@xyz.com'
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> tokens[0].start
    0
    >>> tokens[0].end == len(data)
    True
    >>> data = 'foobar@foo.biz'
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> tokens[0].start
    0
    >>> tokens[0].end == len(data)
    True
    >>> data = 'anony_mous@domain.com'
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> tokens[0].start
    0
    >>> tokens[0].end == len(data)
    True
    >>> data = 'another_long_random_address@example.org'
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> tokens[0].start
    0
    >>> tokens[0].end == len(data)
    True
    >>> data = 'email_address@with.a.long.domain.name.com'
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    1
    >>> tokens[0].start
    0
    >>> tokens[0].end == len(data)
    True
    >>> data = 'foo@bar'
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    0
    >>> data = '123@foo'
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    0
    >>> data = 'xqy@917'
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    0
    >>> data = 'abc@xyz..com'
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    0
    """
    left_context_pattern_string = r"(?:^|[^\p{L}\d\._])"
    right_context_pattern_string = r"(?:$|[^\p{L}\d\._])"
    user_name_group_name = "UserName"
    domain_name_group_name = "DomainName"

    email_pattern_string = r"(?<{0}>[\p{{L}}\d_\.]+)@(?<{1}>[\p{{L}}\d][\p{{L}}\d\-]*(\.[\p{{L}}\d\-]+)+)".format(
        user_name_group_name,
        domain_name_group_name
    )

    def __init__(self):
        super().__init__(OverlapStrategy.NONE,
                         TokenPattern(EmailTokenizer.email_pattern_string,
                                      EmailTokenizer.left_context_pattern_string,
                                      EmailTokenizer.right_context_pattern_string))

    def process_matches(self, matches):
        for m in matches:
            full_match = m.full_match
            user_name_span = RegexBasedTokenizer.get_group_span(full_match, EmailTokenizer.user_name_group_name)
            domain_name_span = RegexBasedTokenizer.get_group_span(full_match, EmailTokenizer.domain_name_group_name)
            s = full_match.string
            yield EmailToken(m.source, m.start, m.end,
                             s[user_name_span[0]:user_name_span[1]],
                             s[domain_name_span[0]:domain_name_span[1]])


if __name__ == '__main__':
    import doctest
    doctest.testmod()
