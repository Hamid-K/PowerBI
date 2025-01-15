class DashedNumbersToken(EntityToken):
    class_id = EntityType.Unknown

    def __eq__(self, other):
        return self._value_based_equality(other)

    def __hash__(self):
        return self._value_based_hash()


class CreditCardNumberToken(DashedNumbersToken):
    class_id = EntityType.CreditCardNumber


class MaskedCreditCardNumberToken(DashedNumbersToken):
    class_id = EntityType.MaskedCreditCardNumber


class SocialSecurityNumberToken(DashedNumbersToken):
    class_id = EntityType.SocialSecurityNumber


class MaskedSocialSecurityNumberToken(DashedNumbersToken):
    class_id = EntityType.MaskedSocialSecurityNumber


class GuidToken(DashedNumbersToken):
    class_id = EntityType.Guid


class DashedNumbersTokenizer(RegexBasedTokenizer):
    """
    >>> data = r"top secret cc number: 1234-5678-4321-8765 do not share!"
    >>> t = DashedNumbersTokenizer()
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    2
    >>> tokens[0].start
    22
    >>> tokens[0].end
    41
    >>> tokens[1].start
    22
    >>> tokens[1].end
    41
    >>> data = r"top secret cc number: XXXX-AAAA-BBBB-8765 do not share!"
    >>> t = DashedNumbersTokenizer()
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    2
    >>> tokens[0].start
    22
    >>> tokens[0].end
    41
    >>> tokens[1].start
    22
    >>> tokens[1].end
    41
    >>> data = r"Avoid identity theft, don't share this social security number: 123-45-6789 with anyone!"
    >>> t = DashedNumbersTokenizer()
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    2
    >>> tokens[0].start
    63
    >>> tokens[0].end
    74
    >>> tokens[1].start
    63
    >>> tokens[1].end
    74
    >>> data = r"Avoid identity theft, don't share this social security number: TTT-SS-6789 with anyone!"
    >>> t = DashedNumbersTokenizer()
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    2
    >>> tokens[0].start
    63
    >>> tokens[0].end
    74
    >>> tokens[1].start
    63
    >>> tokens[1].end
    74
    >>> data = r"I wasted this guid: 496C7D20-D31B-4E30-A72E-FDB327A51888, from wasteaguid.info"
    >>> t = DashedNumbersTokenizer()
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    2
    >>> tokens[0].start
    20
    >>> tokens[0].end
    56
    >>> tokens[1].start
    20
    >>> tokens[1].end
    56
    >>> data = r"I wasted this guid: 3f7e3c55-8f8f-44bd-93a6-c126db2122cd, from wasteaguid.info"
    >>> t = DashedNumbersTokenizer()
    >>> tokens = list(t.match_tokens(data))
    >>> len(tokens)
    2
    >>> tokens[0].start
    20
    >>> tokens[0].end
    56
    >>> tokens[1].start
    20
    >>> tokens[1].end
    56
    """

    alpha_numeric_pattern_string = r"[a-zA-Z0-9]"
    left_context_pattern_string = r"^|[^\p{Pd}]"
    right_context_pattern_string = r"$|[^\p{Pd}]"
    base_dashed_pattern_string = r"(?:%s)+(?:\p{Pd}(?:%s+))+" % (alpha_numeric_pattern_string,
                                                                 alpha_numeric_pattern_string)
    credit_card_number_pattern = r"\d{4}(?:\p{Pd}\d{4}){3}"
    masked_credit_card_number_pattern = r"\p{L}{4}(?:\p{Pd}\p{L}{4}){2}(?:\p{Pd}\d{4})"
    social_security_number_pattern = r"\d{3}\p{Pd}\d{2}\p{Pd}\d{4}"
    masked_social_security_number_pattern = r"\p{L}{3}\p{Pd}\p{L}{2}\p{Pd}\d{4}"
    hex_digit_pattern = r"[A-Fa-f0-9]"
    guid_pattern = "%s{8}\p{Pd}%s{4}\p{Pd}%s{4}\p{Pd}%s{4}\p{Pd}%s{12}" % tuple([hex_digit_pattern] * 5)

    cc_token_pattern = TokenPattern(credit_card_number_pattern,
                                    left_context_pattern_string,
                                    right_context_pattern_string)
    masked_cc_token_pattern = TokenPattern(masked_credit_card_number_pattern,
                                           left_context_pattern_string,
                                           right_context_pattern_string)
    ssn_token_pattern = TokenPattern(social_security_number_pattern,
                                     left_context_pattern_string,
                                     right_context_pattern_string)
    masked_ssn_token_pattern = TokenPattern(masked_social_security_number_pattern,
                                            left_context_pattern_string,
                                            right_context_pattern_string)
    guid_token_pattern = TokenPattern(guid_pattern,
                                      left_context_pattern_string,
                                      right_context_pattern_string)

    def __init__(self):
        super().__init__(OverlapStrategy.NONE,
                         TokenPattern(DashedNumbersTokenizer.base_dashed_pattern_string,
                                      DashedNumbersTokenizer.left_context_pattern_string,
                                      DashedNumbersTokenizer.right_context_pattern_string))

    def process_matches(self, matches):
        for match in matches:
            matched_value = str(match)
            offset = match.start
            yield DashedNumbersToken(match.source, match.start, match.end)

            for m in DashedNumbersTokenizer.cc_token_pattern.matches(matched_value):
                yield CreditCardNumberToken(match.source, offset + m.start, offset + m.end)

            for m in DashedNumbersTokenizer.masked_cc_token_pattern.matches(matched_value):
                yield MaskedCreditCardNumberToken(match.source, offset + m.start, offset + m.end)

            for m in DashedNumbersTokenizer.ssn_token_pattern.matches(matched_value):
                yield SocialSecurityNumberToken(match.source, offset + m.start, offset + m.end)

            for m in DashedNumbersTokenizer.masked_ssn_token_pattern.matches(matched_value):
                yield MaskedSocialSecurityNumberToken(match.source, offset + m.start, offset + m.end)

            for m in DashedNumbersTokenizer.guid_token_pattern.matches(matched_value):
                yield GuidToken(match.source, offset + m.start, offset + m.end)
