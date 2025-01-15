# Common text semantics
import builtins
import regex
from enum import IntEnum
import functools
import itertools
import enum


class Substring(object):
    __slots__ = (
        'content',
        'start',
        'end',
        'value',
        'regex_match_cache',
        'hash_code',
    )

    def __init__(self, content, regex_match_cache=None, start=-1, end=-1):
        self.content = content
        self.start = start if start != -1 else 0
        self.end = end if end != -1 else len(content)
        self.value = None
        self.regex_match_cache = \
            {} if regex_match_cache is None else regex_match_cache
        self.hash_code = None

    def get_value(self):
        """
        >>> s = "abc123"
        >>> Substring(s, start=1, end=5).get_value()
        'bc12'
        >>> Substring(s, start=1).get_value()
        'bc123'
        >>> Substring(s, end=5).get_value()
        'abc12'
        """
        if self.value is None:
            self.value = self.content[self.start:self.end]
        return self.value

    def get_length(self):
        return self.end - self.start

    def __len__(self):
        return self.get_length()

    def __add__(self, other):
        if self is None or other is None:
            return None
        if self.content == other.content and self.end == other.start:
            return Substring(self.content, self.regex_match_cache, self.start, other.end)
        return Substring(self.get_value() + other.get_value())

    def slice(self, slice_start, slice_end):
        """
        >>> sub = Substring("abc123")
        >>> sub.slice(1, 5).content is sub.content
        True
        >>> sub.slice(1, 5).get_value()
        'bc12'
        >>> sub.slice(5, 1) is None
        True
        """
        if slice_start < self.start \
                or slice_end > self.end \
                or slice_start > slice_end:
            return None
        return Substring(self.content,
                         self.regex_match_cache,
                         slice_start,
                         slice_end)

    def __getitem__(self, key):
        """
        >>> s = "abc123"
        >>> sub = Substring("abc123")
        >>> sub[:5].get_value()
        'abc12'
        >>> sub[:5].get_value() == s[:5]
        True
        >>> sub[1:5].get_value()
        'bc12'
        >>> sub[1:5].get_value() == s[1:5]
        True
        >>> sub[1:5][1:3].get_value()
        'c1'
        >>> sub[1:5][1:3].get_value() == s[1:5][1:3]
        True
        >>> sub[1:5][1:].get_value()
        'c12'
        >>> sub[1:5][1:].get_value() == s[1:5][1:]
        True
        >>> len(sub[1])
        1
        >>> sub[0].get_value()
        'a'
        >>> sub[0].get_value() == s[0]
        True
        """
        if isinstance(key, builtins.slice):
            if key.step is not None:
                raise TypeError('Slices with steps are not supported by Substring.')
            if key.start is None:
                start = 0
            else:
                start = key.start if key.start >= 0 else len(self) + key.start
            if key.stop is None:
                stop = len(self)
            else:
                stop = key.stop if key.stop >= 0 else len(self) + key.stop
            return self.slice(start + self.start, stop + self.start)
        elif isinstance(key, int):
            idx = key if key >= 0 else len(self) + key
            idx = idx + self.start
            return self.slice(idx, idx + 1)
        raise Exception('Unrecognized __getitem__ key: ' + key)

    def __hash__(self):
        if self.hash_code is None:
            self.hash_code = hash(self.content) ^ \
                             hash(self.start) ^ \
                             hash(self.end)
        return self.hash_code

    def __eq__(self, other):
        raise NotImplementedError(
            'Substring objects cannot be compared for equality/disequality')

    def __ne__(self, other):
        return not self == other

    def match(self, regex_object):
        """
        >>> r = RegularExpression([r"[a-z]+", r"[0-9]+"])
        >>> sub = Substring("abc123 def456")
        >>> sub.match(r)
        [(0, 6), (7, 13)]
        >>> sub.slice(3, 13).match(r)
        [(7, 13)]
        >>> sub.slice(0, 12).match(r)
        [(0, 6)]
        >>> sub.slice(1, 12).match(r)
        []
        """
        matches = self.regex_match_cache.get(regex_object, None)
        if matches is None:
            matches = regex_object.get_all_matches(self.content)
            self.regex_match_cache[regex_object] = matches
        start = self.start
        end = self.end
        # a filtered list of matches which are contained in this substring
        return list(filter(lambda m: m[0] >= start and m[1] <= end, matches))

    def __repr__(self):
        return 'Substring(%s, start=%d, end=%d)' % (repr(self.content), self.start, self.end)


class RegularExpression(object):
    __slots__ = ('patterns', 'compiled_patterns', 'hash_code', 'regex')

    def __init__(self, patterns):
        self.patterns = patterns
        self.compiled_patterns = [regex.compile(p) for p in self.patterns]
        self.hash_code = None
        self.regex = None

    def get_all_token_matches(self, token_index, content_string):
        """
        >>> r = RegularExpression([r"[a-z]+", r"[0-9]+", r""])
        >>> s = "abc123def456"
        >>> r.get_all_token_matches(0, s)
        [(0, 3), (6, 9)]
        >>> r.get_all_token_matches(1, s)
        [(3, 6), (9, 12)]
        >>> r.get_all_token_matches(2, s) # doctest: +ELLIPSIS
        [(0, 0), ..., (12, 12)]
        """
        max_pos = len(content_string)
        compiled_token = self.compiled_patterns[token_index]
        result = []
        start_pos = 0
        while start_pos <= max_pos:
            m = compiled_token.search(content_string, pos=start_pos)
            if m is None:
                return result
            match_start = m.start()
            match_end = m.end()
            start_pos = \
                match_end if match_end != match_start else match_start + 1
            result.append((m.start(), m.end()))
        return result

    @staticmethod
    def _concatenate_matches(left_matches, right_matches):
        left_ends = {left_matches[i][1]: i for i in range(len(left_matches))}
        return [(left_matches[left_ends[m[0]]][0], m[1])
                for m in right_matches if m[0] in left_ends]

    def get_all_matches(self, content_string):
        """
        >>> s = "abc123 def456"
        >>> RegularExpression([r"[a-z]+"]).get_all_matches(s)
        [(0, 3), (7, 10)]
        >>> RegularExpression([r""]).get_all_matches(s) # doctest: +ELLIPSIS
        [(0, 0), ..., (13, 13)]
        >>> RegularExpression([]).get_all_matches(s) # doctest: +ELLIPSIS
        [(0, 0), ..., (13, 13)]
        """
        compiled_patterns = self.compiled_patterns
        if len(compiled_patterns) == 0:
            return [(i, i) for i in range(len(content_string) + 1)]
        prev_token_matches = self.get_all_token_matches(0, content_string)
        if len(prev_token_matches) == 0:
            return []

        for i in range(1, len(compiled_patterns)):
            current_token_matches = \
                self.get_all_token_matches(i, content_string)
            prev_token_matches = \
                self._concatenate_matches(prev_token_matches,
                                          current_token_matches)
            if len(prev_token_matches) == 0:
                return []

        return prev_token_matches

    def __hash__(self):
        if self.hash_code is None:
            self.hash_code = 0
            for p in self.patterns:
                self.hash_code ^= hash(p)
        return self.hash_code

    def __eq__(self, other):
        if not isinstance(other, RegularExpression):
            return False
        if hash(self) != hash(other):
            return False
        self_patterns = self.patterns
        other_patterns = other.patterns
        if len(self_patterns) != len(other_patterns):
            return False
        for i in range(len(self_patterns)):
            if self_patterns[i] != other_patterns[i]:
                return False

        return True

    def __ne__(self, other):
        return not (self == other)

    def as_regex(self):
        """
        >>> RegularExpression([r"[a-z]+", r"[0-9]+", r""]).as_regex().patterns
        ['[a-z]+[0-9]+']
        """
        if self.regex is None:
            self.regex = RegularExpression([''.join(self.patterns)])
        return self.regex


if __name__ == '__main__':
    import doctest
    doctest.testmod()
