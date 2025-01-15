import regex

class RegexToken(object):
    ''' Like the RegexToken class in the C# semantics.
        A ConstantToken can also be represented as RegexToken, so this is the only
        kind of Token we have in Python.
    '''
    __slots__ = ('name', 'description', '_regex', '_hash_code')
    
    def __init__(self, name, pattern, desc=None):
        self.name = name
        self._hash_code = None
        self.description = name if desc is None else desc
        self._regex = regex.compile(pattern if pattern.startswith('^') else '^(%s)' % pattern)
        
    def __hash__(self):
        if self._hash_code is None:
            self._hash_code = 84859 * hash(self.name) ^ hash(self._regex)
        return self._hash_code
        
    def __eq__(self, other):
        if not isinstance(other, RegexToken):
            return False
        if hash(self) != hash(other):
            return False
        return self.name == other.name and self._regex == other._regex

    def __ne__(self, other):
        return not (self == other)

    def __repr__(self):
        return 'RegexToken({0}: {1})'.format(self.name, str(self._regex.pattern))
    
    def unmatched_suffix_of(self, string):
        '''Returns the unmatched suffix of a string after matching this Token at the beginning,
           or None if there is no match at the beginning.
        '''
        match = self._regex.match(string)
        return None if match is None else string[match.end(0):]


# Matching.Text specific semantics

def no_match():
    return False

def disjunction(match, disjunctiveMatch):
    return match or disjunctiveMatch

def end_of(str):
    return False if str is None else len(str) == 0

def is_null(str):
    return str is None

def suffix_after_token_match(str, token):
    """Returns the unmatched suffix of a string after matching a Token at the beginning,
       or None if there is no match at the beginning.

    >>> suffix_after_token_match('abcd237', RegexToken('Lower', r'[a-z]+'))
    '237'
    >>> suffix_after_token_match('abcd237', RegexToken('Numeric', r'[0-9]+')) is None
    True
    >>> suffix_after_token_match('abcd237', RegexToken('LowerNumeric', r'[a-z0-9]+'))
    ''
    >>> suffix_after_token_match('abcd237', RegexToken('Const[abcd2]', r'^(abcd2)'))
    '37'

    Test consistency with C# RegexToken matching semantics:
    (See PythonTranslator.cs for details on how a C# Token is translated to Python RegexToken.)

    `Alpha+ & Any+` matches `abc123` but not `abcdefg`:
    >>> suffix_after_token_match(
    ...     suffix_after_token_match(
    ...         'abc123',
    ...         RegexToken('Alpha+', r'^(([a-zA-Z])+)')),
    ...     RegexToken('Any+', r'^((.)+)'))
    ''
    >>> suffix_after_token_match(
    ...     suffix_after_token_match(
    ...         'abcdefg',
    ...         RegexToken('Alpha+', r'^(([a-zA-Z])+)')),
    ...     RegexToken('Any+', r'^((.)+)')) is None
    True

    `Alpha{2} & Any+` matches `ab123` but not `abc123`:
    >>> suffix_after_token_match(
    ...     suffix_after_token_match(
    ...         'ab123',
    ...         RegexToken('Alpha{2}', r'^(([a-zA-Z]){2}(?!([a-zA-Z])))')),
    ...     RegexToken('Any+', r'^((.)+)'))
    ''
    >>> suffix_after_token_match(
    ...     suffix_after_token_match(
    ...         'abc123',
    ...         RegexToken('Alpha{2}', r'^(([a-zA-Z]){2}(?!([a-zA-Z])))')),
    ...     RegexToken('Any+', r'^((.)+)')) is None
    True

    The matching semantics are not preserved
    without the negative look-ahead after restricted CharClassTokens:
    >>> suffix_after_token_match(
    ...     suffix_after_token_match(
    ...         'abc123',
    ...         RegexToken('Alpha{2}', r'^(([a-zA-Z]){2})')),
    ...     RegexToken('Any+', r'^((.)+)')) is None
    False
    """
    return None if token is None or str is None else token.unmatched_suffix_of(str)

def nil(strs):
    return None if strs is None or len(strs) > 0 else []

def head(strs):
    return None if strs is None or len(strs) < 1 else strs[0]

def tail(strs):
    return None if strs is None or len(strs) < 1 else strs[1:]

def match_columns(disjunctiveMatch, multiResult):
    return None if multiResult is None else multiResult.insert(0, disjunctiveMatch);multiResult

def if_then_else(cond, thenStr, elseStr):
    return thenStr if cond else elseStr

def labelled_match_columns(labelledDisjunctiveMatch, labelledMultiResult):
    return None if labelledMultiResult is None else labelledMultiResult.insert(0, labelledDisjunctiveMatch);labelledMultiResult


if __name__ == '__main__':
    import doctest
    doctest.testmod()