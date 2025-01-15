import calendar
import datetime
import functools
import itertools
import enum
import regex
import ipaddress

class OverlapStrategy(enum.Enum):
    NONE = 0
    SUBSUMPTION = 1
    STOPATFIRSTSUCCESS = 2


class TokenPatternMatch(object):
    __slots__ = ('full_match', 'source', 'start', 'end', 'left_context_group', 'right_context_group',
                 'content_group', 'token_pattern', '_value')

    def __init__(self, match, source, token_pattern):
        self.full_match = match
        self.source = source
        self.token_pattern = token_pattern
        self.left_context_group = match.groups(TokenPattern.left_context_group_name)
        self.right_context_group = match.groups(TokenPattern.right_context_group_name)
        self.content_group = match.groups(TokenPattern.content_group_name)
        self.start = self.full_match.start(TokenPattern.content_group_name)
        self.end = self.full_match.end(TokenPattern.content_group_name)
        self._value = None

    def __len__(self):
        return self.end - self.start

    def __str__(self):
        return self.get_value()

    def get_value(self):
        if self._value is None:
            self._value = self.source[self.start:self.end]
        return self._value

    def value(self):
        return self.get_value()


class TokenPattern(object):
    left_context_group_name = "__LeftContext__"
    right_context_group_name = "__RightContext__"
    content_group_name = "__Content__"

    __slots__ = ('left_context_pattern', 'right_context_pattern', 'content_pattern',
                 'capture_left_context', 'capture_right_context', 'regex_to_match', 'regex_options')

    def __init__(self, content_pattern, left_context_pattern=None, right_context_pattern=None,
                 capture_left_context=False, capture_right_context=False, *regex_options):
        self.content_pattern = content_pattern
        self.left_context_pattern = left_context_pattern if left_context_pattern is not None else ''
        self.right_context_pattern = right_context_pattern if right_context_pattern is not None else ''
        self.capture_left_context = capture_left_context
        self.capture_right_context = capture_right_context

        regex_parts = []

        if len(self.left_context_pattern) > 0:
            if self.capture_left_context:
                regex_parts.append(r'''(?P<{0}>{1})'''.format(self.left_context_group_name, self.left_context_pattern))
            else:
                regex_parts.append(r'''(?:{0})'''.format(self.left_context_pattern))
        regex_parts.append(r'''(?P<{0}>{1})'''.format(self.content_group_name, self.content_pattern))
        if len(self.right_context_pattern) > 0:
            if self.capture_right_context:
                regex_parts.append(
                    r'''(?P<{0}>{1})'''.format(self.right_context_group_name, self.right_context_pattern))
            else:
                regex_parts.append(r'''(?:{0})'''.format(self.right_context_pattern))
        self.regex_options = functools.reduce(lambda a, b: a | b, regex_options, 0)
        pattern = ''.join(regex_parts)
        self.regex_to_match = regex.compile(pattern, self.regex_options)

    def __eq__(self, other):
        if not isinstance(other, TokenPattern):
            return False
        return str(self.regex_to_match) == str(other.regex_to_match) and self.regex_options == other.regex_options

    def __ne__(self, other):
        return not self.__eq__(other)

    def clone(self):
        return TokenPattern(self.content_pattern, self.left_context_pattern, self.right_context_pattern,
                            self.capture_left_context, self.capture_right_context, self.regex_options)

    def matches(self, s):
        position = 0
        while position < len(s):
            m = self.regex_to_match.search(s, pos=position)
            if m is None:
                return
            yield TokenPatternMatch(m, s, self)
            if position == len(s):
                return
            position = m.end(TokenPattern.content_group_name)


class RegexBasedTokenizer(object):
    __slots__ = ('patterns', 'overlap_strategy')

    def __init__(self, overlap_strategy, *patterns):
        self.overlap_strategy = overlap_strategy
        self.patterns = patterns[:]

    @staticmethod
    def try_parse_int(s):
        try:
            return int(s)
        except ValueError:
            return None

    @staticmethod
    def try_parse_float(s):
        try:
            return float(s)
        except ValueError:
            return None

    @staticmethod
    def get_group_span(match, group_name):
        try:
            return match.span(group_name)
        except IndexError:
            return None

    @staticmethod
    def get_group_spans(match, group_name):
        try:
            return match.spans(group_name)
        except IndexError:
            return None

    def process_matches(self, matches):
        """
        Returns a set of EntityTokens corresponding to
        a set of matches.
        :param matches: The set of TokenPatternMatch objects
        :return: A sequence of EntityToken objects
        """
        raise NotImplementedError('Abstract method call.')

    @staticmethod
    def _default_process_matches_impl(factory, matches):
        return (factory(m.source, m.start, m.end) for m in matches)

    def match_tokens(self, s):
        all_matches = []
        for p in self.patterns:
            all_matches.extend(p.matches(s))
            if self.overlap_strategy == OverlapStrategy.STOPATFIRSTSUCCESS and len(all_matches) > 0:
                break
        processed_matches = self.process_matches(all_matches)
        yield from self.handle_overlaps(processed_matches)

    def handle_overlaps(self, matches):
        if self.overlap_strategy != OverlapStrategy.SUBSUMPTION:
            yield from matches
            return

        def type_extractor(x):
            return str(type(x))

        tokens_by_type = itertools.groupby(sorted(matches, key=type_extractor), key=type_extractor)
        for token_type, token_generator in tokens_by_type:
            sorted_tokens = sorted(token_generator, key=lambda t: (t.start, t.start - t.end))
            best_token_start = 0
            best_token_end = 0
            best_tokens = set()
            for token in sorted_tokens:
                if (token.end > best_token_end) or (token.start == best_token_start and
                                                    token.end == best_token_end and
                                                    token not in best_tokens):
                    yield token
                    best_token_start = token.start
                    best_token_end = token.end
                    best_tokens.add(token)

    def matches(self, s):
        return ((t.start, t.end) for t in self.match_tokens(s))

    def __eq__(self, other):
        if other is None:
            return False
        if self is other:
            return True
        return type(self) == type(other)

    def __hash__(self):
        return hash(str(type(self))) * 317


class EntityType(enum.Enum):
    Unknown = 0
    DomainName = 1
    Url = 2
    CreditCardNumber = 3
    MaskedCreditCardNumber = 4
    SocialSecurityNumber = 5
    MaskedSocialSecurityNumber = 6
    Date = 7
    Time = 8
    EmailAddress = 9
    Path = 10
    FileName = 11
    Guid = 12
    HexadecimalNumber = 13
    IpV4Address = 14
    IpV4CidrAddress = 15
    IpV6Address = 16
    IpV6CidrAddress = 17
    MacAddress = 18
    Currency = 19
    Number = 20
    PhoneNumber = 21

    @staticmethod
    def values():
        return list(EntityType)

    @staticmethod
    def values_except(*values_to_exclude):
        value_set = frozenset(values_to_exclude)
        return filter(lambda v: v not in value_set, EntityType.values())


class EntityToken(object):
    __slots__ = ('source', 'start', 'end', '_value')

    class_id = EntityType.Unknown

    def __init__(self, source, start, end):
        self.source = source
        self.start = start
        self.end = end
        self._value = None

    def __str__(self):
        if self._value is None:
            self._value = self.source[self.start:self.end]
        return self._value

    def __eq__(self, other):
        raise NotImplementedError()

    def __hash__(self):
        raise NotImplementedError()

    def _value_based_equality(self, other):
        if other is None:
            return False
        if other is self:
            return True
        return type(self) == type(other) and str(self) == str(other)

    def _value_based_hash(self):
        return hash(str(self)) ^ hash(str(type(self)))


class TokenizerCollection(object):
    __slots__ = ('tokenizers', '_hash_code')

    def __init__(self, tokenizers):
        self.tokenizers = frozenset(tokenizers)
        self._hash_code = None

    def __eq__(self, other):
        if other is None:
            return False
        if other is self:
            return True
        if type(self) != type(other):
            return False

        return self.tokenizers == other.tokenizers

    def __hash__(self):
        if self._hash_code is None:
            self._hash_code = hash(self.tokenizers) * 12983

    def __iter__(self):
        yield from self.tokenizers


class PrecedenceBasedTokenComparer(object):
    __slots__ = ('has_precedence_over',)
    _instance = None

    @classmethod
    def get_instance(cls):
        if cls._instance is None:
            cls._instance = PrecedenceBasedTokenComparer()
        return cls._instance

    def __init__(self):
        self.has_precedence_over = { x: frozenset() for x in EntityType.values() }
        # number has the lowest precedence
        all_types_except_number = frozenset(EntityType.values_except(EntityType.Number, EntityType.Unknown))
        for t in all_types_except_number:
            self.has_precedence_over[t] |= frozenset([EntityType.Number])
        # dates have precedence over paths
        self.has_precedence_over[EntityType.Date] |= frozenset([EntityType.Path])
        # Urls have precedence over paths
        self.has_precedence_over[EntityType.Url] |= frozenset([EntityType.Path])
        # dates have precedence over file names as well
        self.has_precedence_over[EntityType.Date] |= frozenset([EntityType.FileName])

    def compare(self, obj1, obj2):
        if obj1 is obj2:
            return 0
        if obj1 is None:
            return -1
        if obj2 is None:
            return 1
        if (not isinstance(obj1, EntityToken)) or (not isinstance(obj2, EntityToken)):
            return 0

        x_type = type(obj1).class_id
        y_type = type(obj2).class_id

        if y_type in self.has_precedence_over[x_type]:
            return 1
        if x_type in self.has_precedence_over[y_type]:
            return -1
        return 0


def _get_top_tokens_from_lattice(tokens):
    tokens = list(tokens)
    incoming_precedence_edges = [list() for _ in range(len(tokens))]
    outgoing_precedence_edges = [list() for _ in range(len(tokens))]

    comparer = PrecedenceBasedTokenComparer.get_instance()

    for i in range(len(tokens)):
        for j in range(i + 1, len(tokens)):
            cmp_result = comparer.compare(tokens[i], tokens[j])
            if cmp_result > 0:
                outgoing_precedence_edges[i].append(j)
                incoming_precedence_edges[j].append(i)
            elif cmp_result < 0:
                outgoing_precedence_edges[j].append(i)
                incoming_precedence_edges[i].append(j)

    indices_to_consider = set(range(len(tokens)))

    while len(indices_to_consider) > 0:
        undominated_indices = list(filter(lambda x: len(incoming_precedence_edges[x]) == 0, indices_to_consider))
        if len(undominated_indices) == 0:
            return

        for idx in undominated_indices:
            yield tokens[idx]
            indices_to_consider.remove(idx)
            incoming_edges_to_remove = []
            outgoing_edges_to_remove = []

            for i in outgoing_precedence_edges[idx]:
                indices_to_consider.remove(i)

                for j in outgoing_precedence_edges[i]:
                    incoming_edges_to_remove.append((j, i))
                for j in incoming_precedence_edges[i]:
                    outgoing_precedence_edges[j].remove(i)

            for i, j in incoming_edges_to_remove:
                incoming_precedence_edges[i].remove(j)
            for i, j in outgoing_edges_to_remove:
                outgoing_precedence_edges[i].remove(j)


def _resolve_token_subsumption_by_precedence(tokens):
    sorted_tokens = sorted(tokens, key=lambda t: (t.start, -t.end))
    idx = 0
    while idx < len(sorted_tokens):
        tokens_to_check_for_subsumption = []
        first_token = sorted_tokens[idx]
        idx += 1
        tokens_to_check_for_subsumption.append(first_token)
        covered_until = first_token.end
        while idx < len(sorted_tokens) and sorted_tokens[idx].end <= covered_until:
            tokens_to_check_for_subsumption.append(sorted_tokens[idx])
            idx += 1

        grouped_tokens = itertools.groupby(sorted(tokens_to_check_for_subsumption, key=lambda t: str(type(t))),
                                           key=lambda t: str(type(t)))
        final_tokens = []
        for key, tokens in grouped_tokens:
            spans = set()
            for token in tokens:
                span = (token.start, token.end)
                if span in spans:
                    continue
                else:
                    spans.add(span)
                    final_tokens.append(token)

        yield from _get_top_tokens_from_lattice(final_tokens)


class TokenizerCollectionToExtractor(object):
    __slots__ = ('tokenizers', 'entity_type')
    supported_entity_types = frozenset(EntityType.values_except(EntityType.Unknown))

    def __init__(self, entity_type, tokenizers):
        self.tokenizers = TokenizerCollection(tokenizers)
        self.entity_type = entity_type

    def extract(self, s):
        all_tokens = []
        for tokenizer in self.tokenizers:
            all_tokens.extend(filter(lambda tok: type(tok).class_id in
                                     TokenizerCollectionToExtractor.supported_entity_types,
                                     tokenizer.match_tokens(s)))
        for token in _resolve_token_subsumption_by_precedence(all_tokens):
            if type(token).class_id == self.entity_type:
                yield (token.start, token.end)

