# Common PROSE concepts
import functools
import itertools
import sys


class RepeatableEnumerable(object):
    '''A repeatable enumerable. Used to wrap Python's one-shot generators
    to produce something that can be re-enumerated. To more closely model
    C#'s enumerables'''
    __slots__ = ('factory', 'factory_args')

    def __init__(self, factory, *factory_args):
        self.factory = factory
        self.factory_args = factory_args

    def __iter__(self):
        _generator = self.factory(*self.factory_args)
        yield from _generator


class ChainedEnumerable(object):
    '''Like itertools.chain, except that it's repeatable'''
    __slots__ = ('left', 'right')

    def __init__(self, left, right):
        self.left = left
        self.right = right

    def __iter__(self):
        yield from self.left
        yield from self.right


class Optional(object):
    '''A value that may or may not be present. Different from None because
    the value that is present might be None.'''
    __slots__ = ('has_value', 'value')

    def __init__(self, has_value, value):
        self.has_value = has_value
        self.value = value if has_value else None

    def __eq__(self, other):
        if isinstance(other, self.__class__):
            if self.has_value != other.has_value:
                return False
            if self.has_value:
                return self.value == other.value
            else:
                return True
        else:
            return False

    def __hash__(self):
        if self.has_value:
            return hash(self.value)
        else:
            return hash(self.has_value)

    @staticmethod
    def some(value):
        return Optional(True, value)

    _nothing = None
    @staticmethod
    def nothing():
        if Optional._nothing is None:
            Optional._nothing = Optional(False, None)
        return Optional._nothing


def prose_concat(a0, a1):
    """Return the concatenation of two string arrays

    >>> list(prose_concat(['a','b'],['c']))
    ['a', 'b', 'c']
    """
    return ChainedEnumerable(a0, a1)


def prose_conjunct(a0, a1):
    """Return the conjunction of two bools.

    >>> prose_conjunct(True, True)
    True
    >>> prose_conjunct(True, False)
    False
    >>> prose_conjunct(False, None) is None
    True
    """
    if type(a0) != bool or type(a1) != bool:
        return None
    return a0 and a1


def prose_disjunct(a0, a1):
    """Return the disjunction of two bools.

    >>> prose_disjunct(True, True)
    True
    >>> prose_disjunct(True, False)
    True
    >>> prose_disjunct(True, None) is None
    True
    """
    if type(a0) != bool or type(a1) != bool:
        return None
    return a0 or a1


def prose_filter(predicate, sequence):
    """Filter a sequence down to those for which a predicate holds, lazily.

    >>> list(prose_filter(lambda i: i < 3, range(10)))
    [0, 1, 2]
    """
    return RepeatableEnumerable(filter, predicate, sequence)


def prose_filterint(slice_tuple, sequence):
    """Filter a sequence of those items starting after slice_tuple[0] and
    then every slice_tuple[1]st item.

    >>> list(prose_filterint((2,3), range(10)))
    [2, 5, 8]
    """
    return RepeatableEnumerable(itertools.islice, sequence, slice_tuple[0],
                                len(sequence), slice_tuple[1])


def prose_first(predicate, sequence):
    """Return the first element of a sequence for which a predicate holds, lazily.

    >>> prose_first(lambda i: i > 3, range(10))
    4
    >>> prose_first(lambda i: i > 33, range(10)) is None
    True
    """
    filtered_sequence = filter(predicate, sequence)
    return next(filtered_sequence, None)


def prose_if(condition, if_branch, else_branch):
    """If condition, return if_branch, else else_branch.

    >>> prose_if(True, 'spam', 'eggs')
    'spam'
    >>> prose_if(False, 'spam', 'eggs')
    'eggs'
    >>> prose_if(None, 'spam', 'eggs') is None
    True
    """
    if condition is None:
        return None
    return if_branch if condition else else_branch


def prose_kth(sequence, k):
    """Return the kth element of sequence.

    >>> prose_kth(range(10), 2)
    2
    >>> prose_kth(range(10), -2)
    8
    >>> prose_kth(range(10), 20) is None
    True
    >>> prose_kth(range(10), -20) is None
    True
    """
    k = int(k)
    if k >= 0:
        return next(itertools.islice(sequence, k, None), None)
    else:
        # negative indexing requires us to make the list concrete
        sequence_as_list = list(sequence)
        if k + len(sequence_as_list) <= 0:
            return None
        else:
            return sequence_as_list[k]


def prose_map(f, sequence):
    """Return a transformed sequence where f was applied to every element, lazily.

    [False, True, True]
    """
    return RepeatableEnumerable(map, f, sequence)


def prose_pair(a0, a1):
    """Return a two-element tuple.

    >>> prose_pair('spam', 'eggs')
    ('spam', 'eggs')
    """
    return (a0, a1)


def prose_reduce(sequence_of_sequences):
    """Flatten a list of lists to a single list.

    >>> prose_reduce([['a','b'],[],['c']])
    ['a', 'b', 'c']
    """
    return functools.reduce(list.__add__, sequence_of_sequences)


def prose_selectmany(f, sequence):
    """Apply f to every element of a sequence and flatten the list results.

    >>> prose_selectmany(str.split, ['help me', '', 'out', 'here please'])
    ['help', 'me', 'out', 'here', 'please']
    """
    return functools.reduce(list.__add__, map(f, sequence))


def prose_split(sequence, predicate):
    """Not implemented!
    TODO: Implement this once we have some acceptance tests for the C# version.

    >>> prose_split('abc123 def', lambda t: True)
    Traceback (most recent call last):
        ...
    NotImplementedError: prose_split has not been implemented
    """

    # Here's a translation of what the C# version of this operator seems to do
    """
    seq = sequence
    split_seq = []
    begin = 0
    for i in range(1, len(seq)):
        same = predicate((seq[i - 1], seq[i]))
        if not same:
            split_seq.append(seq[begin:i])
            begin = i
    split_seq.append(seq[begin:])
    return split_seq
    """
    raise NotImplementedError("prose_split has not been implemented")


def prose_switch(condition, if_value, else_value):
    """If condition is bool True, return if_value, else else_value.
    Unlike prose_if, prose_switch considers a non-bool condition to select
    else_value rather than returning None.

    >>> prose_switch(True, 'spam', 'eggs')
    'spam'
    >>> prose_switch(False, 'spam', 'eggs')
    'eggs'
    >>> prose_switch(None, 'spam', 'eggs')
    'eggs'
    """
    cond = isinstance(condition, bool) and condition
    return if_value if cond else else_value


def prose_takewhile(predicate, sequence):
    """Return a sequence of first items while a predicate is true.

    >>> list(prose_takewhile(lambda i: i < 3, range(10)))
    [0, 1, 2]
    """
    return RepeatableEnumerable(itertools.takewhile, predicate, sequence)


def prose_windowed(sequence):
    """Return a sequence of adjacent pairs from the sequence.

    >>> list(prose_windowed(range(5)))
    [(0, 1), (1, 2), (2, 3), (3, 4)]
    """
    return RepeatableEnumerable(zip, sequence, RepeatableEnumerable(itertools.islice, sequence, 1, None))



# utility functions for creating modules
if sys.version_info.major >= 3 and sys.version_info.minor < 5:
    import imp
elif sys.version_info.major >= 3 and sys.version_info.minor >= 5:
    import importlib
    import importlib.util
    import importlib.machinery
else:
    raise RuntimeError('Need Python 3 or greater!')


def create_blank_module(name):
    if sys.version_info.major < 3:
        raise RuntimeError('Need Python 3 or greater!')
    if sys.version_info.minor < 5:
        m = imp.new_module(name)
    else:
        m = importlib.util.module_from_spec(
                importlib.machinery.ModuleSpec(name, None))

    sys.modules[name] = m
    return m


def execute_in_named_module(program_string, module_name):
    if module_name is not None:
        m = sys.modules.get(module_name, None)
        if m is None:
            m = create_blank_module(module_name)
        exec(program_string, m.__dict__)
    else:
        exec(program_string, globals())


if __name__ == '__main__':
    import doctest
    doctest.testmod()
