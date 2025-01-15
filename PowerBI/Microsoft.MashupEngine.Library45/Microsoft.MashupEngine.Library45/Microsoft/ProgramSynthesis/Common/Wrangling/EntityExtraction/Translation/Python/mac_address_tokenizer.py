class MacAddressToken(EntityToken):
    class_id = EntityType.MacAddress

    def __eq__(self, other):
        return self._value_based_equality(other)

    def __hash__(self):
        return self._value_based_hash()


class MacAddressTokenizer(RegexBasedTokenizer):
    """
    >>> t = MacAddressTokenizer()
    >>> data = r"there is a mac 01-23-45-67-89-ab address buried in here!"
    >>> tokens = list(filter(lambda x: type(x) == MacAddressToken, t.match_tokens(data)))
    >>> len(tokens)
    1
    >>> str(tokens[0])
    '01-23-45-67-89-ab'

    >>> data = r"there is a mac 01:23:45:67:89:ab address buried in here!"
    >>> tokens = list(filter(lambda x: type(x) == MacAddressToken, t.match_tokens(data)))
    >>> len(tokens)
    1
    >>> str(tokens[0])
    '01:23:45:67:89:ab'

    >>> data = r"there is a mac 0123.4567.89ab address buried in here!"
    >>> tokens = list(filter(lambda x: type(x) == MacAddressToken, t.match_tokens(data)))
    >>> len(tokens)
    1
    >>> str(tokens[0])
    '0123.4567.89ab'
    """
    pair_component_pattern_string = r"[A-Fa-f0-9]{2}"
    quad_component_pattern_string = r"[A-Fa-f0-9]{4}"
    separator_pattern_string = r"(?<Separator>[\.\:\-])"
    pair_mac_address_pattern_string = r"{0}{1}{2}((?P=Separator){3}){{4}}".format(
        pair_component_pattern_string,
        separator_pattern_string,
        pair_component_pattern_string,
        pair_component_pattern_string
    )
    quad_mac_address_pattern_string = r"{0}{1}{2}(?P=Separator){3}".format(
        quad_component_pattern_string,
        separator_pattern_string,
        quad_component_pattern_string,
        quad_component_pattern_string
    )

    left_context_pattern_string = r"^|[^\d\w\.\-\:]"
    right_context_pattern_string = r"$|[^\d\w\.\-\:]"

    def __init__(self):
        super().__init__(OverlapStrategy.NONE,
                         TokenPattern(MacAddressTokenizer.pair_mac_address_pattern_string,
                                      MacAddressTokenizer.left_context_pattern_string,
                                      MacAddressTokenizer.right_context_pattern_string),
                         TokenPattern(MacAddressTokenizer.quad_mac_address_pattern_string,
                                      MacAddressTokenizer.left_context_pattern_string,
                                      MacAddressTokenizer.right_context_pattern_string))

    def process_matches(self, matches):
        yield from self._default_process_matches_impl(MacAddressToken, matches)