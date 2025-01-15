class IpAddressToken(EntityToken):
    __slots__ = ('address',)

    class_id = EntityType.Unknown

    def __init__(self, source, start, end, address):
        super().__init__(source, start, end)
        self.address = address

    @staticmethod
    def create(source, start, end, address, subnet_bits):
        if address.version == 4 and subnet_bits is None:
            return IpV4AddressToken(source, start, end, address)
        if address.version == 4 and subnet_bits is not None:
            return IpV4CidrAddressToken(source, start, end, address, subnet_bits)
        if address.version == 6 and subnet_bits is None:
            return IpV6AddressToken(source, start, end, address)
        if address.version == 6 and subnet_bits is not None:
            return IpV6CidrAddressToken(source, start, end, address, subnet_bits)

    def __eq__(self, other):
        return self._value_based_equality(other)

    def __hash__(self):
        return self._value_based_hash()


class IpV4AddressToken(IpAddressToken):
    class_id = EntityType.IpV4Address


class IpV4CidrAddressToken(IpV4AddressToken):
    __slots__ = ('subnet_bits',)

    class_id = EntityType.IpV4CidrAddress

    def __init__(self, source, start, end, address, subnet_bits):
        super().__init__(source, start, end, address)
        self.subnet_bits = subnet_bits

    def __eq__(self, other):
        if other is None:
            return False
        if other is self:
            return True
        if type(other) != type(self):
            return False
        return self.address == other.address and self.subnet_bits == other.subnet_bits

    def __hash__(self):
        return hash(self.address) ^ hash(type(self)) ^ hash(self.subnet_bits)


class IpV6AddressToken(IpAddressToken):
    class_id = EntityType.IpV6Address


class IpV6CidrAddressToken(IpV6AddressToken):
    __slots__ = ('subnet_bits',)

    class_id = EntityType.IpV6CidrAddress

    def __init__(self, source, start, end, address, subnet_bits):
        super().__init__(source, start, end, address)
        self.subnet_bits = subnet_bits

    def __eq__(self, other):
        if other is None:
            return False
        if other is self:
            return True
        if type(other) != type(self):
            return False
        return self.address == other.address and self.subnet_bits == other.subnet_bits

    def __hash__(self):
        return hash(self.address) ^ hash(type(self)) ^ hash(self.subnet_bits)


class IpAddressTokenizer(RegexBasedTokenizer):
    """
    >>> t = IpAddressTokenizer()
    >>> data = r"moore 192.168.3.2 noise"
    >>> tokens = t.match_tokens(data)
    >>> ip_address_tokens = list(filter(lambda x: type(x) == IpV4AddressToken, tokens))
    >>> len(ip_address_tokens)
    1
    >>> str(ip_address_tokens[0])
    '192.168.3.2'

    >>> data = r"moore 10.14.64.32/24 noyce"
    >>> tokens = t.match_tokens(data)
    >>> ip_address_tokens = list(filter(lambda x: type(x) == IpV4CidrAddressToken, tokens))
    >>> len(ip_address_tokens)
    1
    >>> str(ip_address_tokens[0])
    '10.14.64.32/24'

    >>> data = r"moore 1234.5678.0123.4567 noyce"
    >>> tokens = t.match_tokens(data)
    >>> len(list(tokens))
    0

    >>> data = r"moore 2001:0db8:85a3:0000:0000:8a2e:0370:7334 noyce"
    >>> tokens = t.match_tokens(data)
    >>> ip_address_tokens = list(filter(lambda x: type(x) == IpV6AddressToken, tokens))
    >>> len(ip_address_tokens)
    1
    >>> str(ip_address_tokens[0])
    '2001:0db8:85a3:0000:0000:8a2e:0370:7334'

    >>> data = r"moore 2001:db8::1:0:0:1 noyce"
    >>> tokens = t.match_tokens(data)
    >>> ip_address_tokens = list(filter(lambda x: type(x) == IpV6AddressToken, tokens))
    >>> len(ip_address_tokens)
    1
    >>> str(ip_address_tokens[0])
    '2001:db8::1:0:0:1'

    >>> data = r"moore ::1 noyce"
    >>> tokens = t.match_tokens(data)
    >>> ip_address_tokens = list(filter(lambda x: type(x) == IpV6AddressToken, tokens))
    >>> len(ip_address_tokens)
    1
    >>> str(ip_address_tokens[0])
    '::1'

    >>> data = r"moore :: noyce"
    >>> tokens = t.match_tokens(data)
    >>> ip_address_tokens = list(filter(lambda x: type(x) == IpV6AddressToken, tokens))
    >>> len(ip_address_tokens)
    1
    >>> str(ip_address_tokens[0])
    '::'

    >>> data = r"gibberish 2001:db8::1:0:0:1/32 noyce"
    >>> tokens = t.match_tokens(data)
    >>> ip_address_tokens = list(filter(lambda x: type(x) == IpV6CidrAddressToken, tokens))
    >>> len(ip_address_tokens)
    1
    >>> str(ip_address_tokens[0])
    '2001:db8::1:0:0:1/32'

    >>> data = r"2001:db8::234::4567 noyce"
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    0
    """
    dotted_quad_component_pattern_string = r'\d{1,3}'
    hex_pattern_string = r'[0-9a-fA-F]'
    colon_separated_component_pattern_string = r"{0}{{0,4}}".format(hex_pattern_string)
    ip_v4_left_context_pattern_string = r"(?:^|[^\.\d])"
    ip_v4_right_context_pattern_string = r"(?:$|[^\.\d])"
    ip_v6_left_context_pattern_string = r"(?:^|[^\:a-fA-F0-9])"
    ip_v6_right_context_pattern_string = r"(?:$|[^\:\da-fA-F0-9])"
    subnet_bits_group_name = r"SubnetBits"
    address_group_name = r"Address"

    ip_v4_address_pattern_string = r"(?<{0}>{1}(?:\.{2}){{3}})(?:/(?<{3}>\d{{1,2}}))?".format(
        address_group_name,
        dotted_quad_component_pattern_string,
        dotted_quad_component_pattern_string,
        subnet_bits_group_name
    )

    ip_v6_address_pattern_string = r"(?<{0}>{1}(?:\:{2})+)(?:/(?<{3}>\d{{1,3}}))?".format(
        address_group_name,
        colon_separated_component_pattern_string,
        colon_separated_component_pattern_string,
        subnet_bits_group_name
    )

    def __init__(self):
        self._ip_v4_address_token_pattern = TokenPattern(IpAddressTokenizer.ip_v4_address_pattern_string,
                                                         IpAddressTokenizer.ip_v4_left_context_pattern_string,
                                                         IpAddressTokenizer.ip_v4_right_context_pattern_string)
        self._ip_v6_address_token_pattern = TokenPattern(IpAddressTokenizer.ip_v6_address_pattern_string,
                                                         IpAddressTokenizer.ip_v6_left_context_pattern_string,
                                                         IpAddressTokenizer.ip_v6_right_context_pattern_string)
        super().__init__(OverlapStrategy.SUBSUMPTION,
                         self._ip_v4_address_token_pattern,
                         self._ip_v6_address_token_pattern)

    def process_matches(self, matches):
        for m in matches:
            s = m.source
            address_span = RegexBasedTokenizer.get_group_span(m.full_match, IpAddressTokenizer.address_group_name)
            if address_span is None:
                continue
            address_string = s[address_span[0]:address_span[1]]
            try:
                address = ipaddress.ip_address(address_string)
            except ValueError:
                return
            subnet_span = RegexBasedTokenizer.get_group_span(m.full_match, IpAddressTokenizer.subnet_bits_group_name)
            subnet_bits = None if subnet_span is None or subnet_span[0] == subnet_span[1] else \
                int(s[subnet_span[0]:subnet_span[1]])
            yield IpAddressToken.create(m.source, m.start, m.end, address, subnet_bits)
