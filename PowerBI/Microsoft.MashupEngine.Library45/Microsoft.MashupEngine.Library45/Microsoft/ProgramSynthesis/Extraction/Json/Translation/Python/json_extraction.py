import json
import collections
import functools
import itertools

def select_many(list_of_lists):
    """Return a flattened iterator over the given iterators.

    >>> select_many([range(0,3), range(100,103)])
    [0, 1, 2, 100, 101, 102]
    """
    return list(itertools.chain.from_iterable(list_of_lists))


# tree structure for parsed json
def _make_lazy_json_tree(parsed_json_object, parent=None):
    if isinstance(parsed_json_object, list):
        return LazyJArray(parsed_json_object, parent)
    elif isinstance(parsed_json_object, dict) or isinstance(parsed_json_object, collections.OrderedDict):
        return LazyJObject(parsed_json_object, parent)
    else:
        return LazyJValue(parsed_json_object, parent)


def load_lazy_json_tree_from_string(json_string, start_delimiter=None, end_delimiter=None, handleInvalid=True):
    if (start_delimiter is not None) or (end_delimiter is not None):
        start_offset = len(start_delimiter) if (start_delimiter is not None) else 0
        end_offset = len(end_delimiter) if (end_delimiter is not None) else 0		
        json_string = trim_delimiter(json_string, start_offset, end_offset)
    json_root = repair_loads(json_string, collections.OrderedDict, handleInvalid)
    return _make_lazy_json_tree(json_root)


def trim_delimiter(line, start_offset, end_offset):
    if len(line) <= start_offset + end_offset:
        return None
    if (end_offset == 0):
        return line[start_offset:]
    return line[start_offset:-end_offset]


def repair_loads(json_string, object_pairs_hook, handleInvalid):
    try:
        return json.loads(json_string, object_pairs_hook=object_pairs_hook)
    except Exception:
        if handleInvalid:
            return repair_json(json_string, object_pairs_hook)
        return None


def repair_load(json_fp, object_pairs_hook, handleInvalid):
    try:
        return json.load(json_fp, object_pairs_hook=object_pairs_hook)
    except Exception:
        if handleInvalid:
            json_fp.seek(0)
            return repair_json(json_fp.read(), object_pairs_hook)
        return None


def repair_json(json_string, object_pairs_hook):
    if (not json_string or json_string.isspace()):
        return None
    stack = []
    trailingCommas = []
    withinQuote = False
    lastCommaIndex = -1
    lastSafePos = 0
    pos = -1
    it = iter(json_string)
    while True:
        try:
            c = next(it)
            pos += 1
        except StopIteration:
            break

        if c == '{':
            if not withinQuote:
                lastSafePos = pos
                stack.append('}')
        elif c == '[':
            if not withinQuote:
                lastSafePos = pos
                stack.append(']')
        elif c == '}' or c == ']':
            if not withinQuote:
                lastSafePos = pos
                if len(stack) == 0:
                    return None # no opening { or [
                stack.pop()
                if lastCommaIndex >= 0:
                    trailingCommas.append(lastCommaIndex)
        elif c == '\\':
            try:
                c = next(it)
                pos += 1
            except StopIteration:
                break
        elif c == '"':
            withinQuote = not withinQuote
        elif c == ',':
            if not withinQuote:
                lastSafePos = pos - 1
                lastCommaIndex = pos
        
        # Reset comma index
        if (not withinQuote and c != ',' and not c.isspace()):
            lastCommaIndex = -1

    # NOTE: [Python specific] Remove trailing commas because python does not support it
    for index in reversed(trailingCommas):
        json_string = json_string[:index] + json_string[index + 1:]

    append = ''.join(stack[::-1]) # reverse the stack

    # Try simple fix first by adding the missing closing ']' or '}'
    try:
        # Note: do this to make it consistent with the reference JSON parser
        if json_string.rstrip().endswith(':'):
            append = 'null ' + append       
        return json.loads(json_string + append, object_pairs_hook=object_pairs_hook)
    except Exception:
        # Trim the truncated property or array element
        json_string = json_string[:lastSafePos+1]
        return json.loads(json_string + append, object_pairs_hook=object_pairs_hook)


def token_to_string(token, top_level=True):
    if isinstance(token, LazyJValue):
        if top_level:
            return str(token)
        else:
            return json.dumps(token.parsed_json_object)
    else:
        return str(token)
            

class LazyJToken(object):
    __slots__ = ('parent', 'parsed_json_object', 'kind', 'children')

    def __init__(self, parsed_json_object, kind, parent=None):
        self.parsed_json_object = parsed_json_object
        self.parent = parent
        self.kind = kind
        self.children = None

    def __str__(self):
        raise NotImplementedError('Abstract method __str__() has not been implemented in type: ' + str(type(self)))

    def __iter__(self):
        raise NotImplementedError('Abstract method __iter__() has not been implemented in type: ' + str(type(self)))

    def __getitem__(self, item):
        raise NotImplementedError('Abstract method __getitem__() has not been implemented in type: ' + str(type(self)))

    def __len__(self):
        raise NotImplementedError('Abstract method __len__() has not been implemented in type: ' + str(type(self)))

    def select_tokens(self, path):
        result = [self]
        for step in path.steps:
            result = select_many(step.apply(t) if t is not None else [] for t in result)
            if len(result) == 0:
                return []
        return result


class LazyJArray(LazyJToken):
    def __init__(self, parsed_json_object, parent=None):
        super().__init__(parsed_json_object, 'JArray', parent)
        self.children = [None] * len(self.parsed_json_object)

    def __str__(self):
        if len(self) == 0:
            return '[]'
        eles = [token_to_string(c, False) for c in self]
        return '[' + ','.join(eles) + ']'

    def __iter__(self):
        for i in range(len(self.children)):
            if self.children[i] is None and self.parsed_json_object[i] is not None:
                self.children[i] = _make_lazy_json_tree(self.parsed_json_object[i], self)
            yield self.children[i]

    def __getitem__(self, item):
        if not isinstance(item, int) or item < 0 or item >= len(self.children):
            return None

        if self.children[item] is None and self.parsed_json_object[item] is not None:
            self.children[item] = _make_lazy_json_tree(self.parsed_json_object[item], self)
        return self.children[item]

    def __len__(self):
        return len(self.children)


class LazyJProperty(LazyJToken):
    __slots__ = ('property_key',)

    def __init__(self, key, parsed_json_object, parent=None):
        super().__init__(parsed_json_object, 'JProperty', parent)
        self.property_key = LazyJValue(key, self)
        self.children = None

    def __str__(self):
        self._populate_children()
        return token_to_string(self.property_key, False) + ':' + token_to_string(self.children, False)

    def _populate_children(self):
        if self.children is None:
            self.children = _make_lazy_json_tree(self.parsed_json_object, self)

    def __iter__(self):
        self._populate_children()
        yield self.children

    def __getitem__(self, item):
        self._populate_children()
        if isinstance(item, LazyJValue):
            if item == self.property_key:
                return self.children
            else:
                return None
        elif isinstance(item, str) and item == self.property_key.parsed_json_object:
            return self.children
        else:
            return None

    def __len__(self):
        return 1

    def key(self):
        return self.property_key

    def value(self):
        self._populate_children()
        return self.children


class LazyJObject(LazyJToken):
    __slots__ = ('fully_converted',)

    def __init__(self, parsed_json_object, parent=None):
        super().__init__(parsed_json_object, 'JObject', parent)
        self.children = collections.OrderedDict()
        self.fully_converted = False

    def __str__(self):
        if len(self) == 0:
            return '{}'
        eles = [token_to_string(x, False) for x in self]
        return '{' + ','.join(eles) + '}'

    def __iter__(self):
        if self.fully_converted:
            for k, prop in self.children:
                yield prop
        else:
            for k, v in self.parsed_json_object.items():
                r = self.children.get(k, None)
                if r is None:
                    r = LazyJProperty(k, v, self)
                    self.children[k] = r
                yield r
            self.fully_converted = True

    def __len__(self):
        return len(self.parsed_json_object)

    def __getitem__(self, item):
        if item in self.children:
            return self.children[item].value()
        if self.fully_converted:
            return None
        if item not in self.parsed_json_object:
            return None
        v = self.parsed_json_object[item]
        r = LazyJProperty(item, v, self)
        self.children[item] = r
        if len(self.children) == len(self.parsed_json_object):
            self.fully_converted = True
        return r.value()

    def get(self, item, on_missing=None):
        try:
            return self[item]
        except KeyError:
            return on_missing


class LazyJValue(LazyJToken):
    def __init__(self, value, parent=None):
        super().__init__(value, 'JValue', parent)

    def __getitem__(self, item):
        return None

    def __str__(self):
        if isinstance(self.parsed_json_object, bool):
            return 'true' if self.parsed_json_object else 'false'
        if self.parsed_json_object is None:
            return 'null'
        return str(self.parsed_json_object)

    def __iter__(self):
        return

    def __eq__(self, other):
        if not isinstance(other, LazyJValue):
            return False
        return self.parsed_json_object == other.parsed_json_object

    def __hash__(self):
        return hash(self.parsed_json_object) * 113

    def __len__(self):
        return 0


class JPathStep(object):
    __slots__ = ['step_kind']

    def __init__(self, step_kind):
        self.step_kind = step_kind

    def _raise_abstract_call_exception(self, method_name):
        raise NotImplementedError('Abstract method ' + method_name +
                                  ' has not been implemented in type: ' + str(type(self)))

    def __str__(self):
        self._raise_abstract_call_exception('__str__()')

    def apply(self, token):
        self._raise_abstract_call_exception('apply()')


class AccessStep(JPathStep):
    __slots__ = ['key']

    def __init__(self, key):
        super().__init__('Access')
        self.key = key

    def __str__(self):
        return 'AccessStep("{0}")'.format(self.key)

    def apply(self, token):
        if not isinstance(token, LazyJObject):
            return None
        if token is None:
            return []
        v = token.get(self.key, None)
        if v is None:
            return []
        return [v]


class CurrentStep(JPathStep):
    def __init__(self):
        super().__init__('Current')

    def __str__(self):
        return 'CurrentStep'

    def apply(self, token):
        return [token]


class IndexStep(JPathStep):
    __slots__ = ['k']

    def __init__(self, k):
        super().__init__('Index')
        self.k = k

    def __str__(self):
        return 'IndexStep({0})'.format(self.k)

    def apply(self, token):
        if not isinstance(token, LazyJArray) or len(token) <= self.k:
            return []
        return [token[self.k]]


class ParentStep(JPathStep):
    def __init__(self):
        super().__init__('Parent')

    def __str__(self):
        return 'ParentStep'

    def apply(self, token):
        return token.parent


class PropertyKeyStep(JPathStep):
    def __init__(self):
        super().__init__('PropertyKey')

    def __str__(self):
        return 'PropertyKeyStep'

    def apply(self, token):
        if not isinstance(token, LazyJProperty):
            return []
        return [token.key()]


class PropertyValueStep(JPathStep):
    def __init__(self):
        super().__init__('PropertyValue')

    def __str__(self):
        return 'PropertyValueStep'

    def apply(self, token):
        if not isinstance(token, LazyJProperty):
            return []
        return [token.value()]


class SinglePropertyStep(JPathStep):
    def __init__(self):
        super().__init__('SingleProperty')

    def __str__(self):
        return 'SinglePropertyStep'

    def apply(self, token):
        if isinstance(token, LazyJObject):
            if len(token) != 1:
                return []
            first = token[0]
            if not isinstance(first, LazyJProperty):
                return []
            return [first.value()]
        else:
            return []


class StarStep(JPathStep):
    def __init__(self):
        super().__init__('Star')

    def __str__(self):
        return 'StarStep'

    def apply(self, token):
        if isinstance(token, LazyJObject) or isinstance(token, LazyJArray):
            return list(token)
        return []


class JPath(object):
    def __init__(self, steps):
        self.steps = steps


class JsonRegion(object):
    __slots__ = ['token', 'value']

    def __init__(self):
        pass

    @staticmethod
    def create(token):
        r = JsonRegion()
        r.token = token
        r.value = None
        return r

    def get_value(self):
        if self.value is None:
            if self.token.parsed_json_object is None:
                return None
            self.value = token_to_string(self.token)
        return self.value


# semantics for Extraction.Json
def sequence(id, regions):
    return SequenceOutput(id, [FieldOutput(id, x) for x in regions])


def dummy_sequence(sequenceBody):
    return SequenceOutput("", sequenceBody)


def struct(region, struct_body_rec):
    return FieldOutput("", region, struct_body_rec)


def field(region, id, select_region):
    return FieldOutput(id, select_region)


def concat(output, struct_body_rec):
    return to_list(output) + struct_body_rec


def to_list(output):
    return [output]


def empty():
    return []


def select_region(region, path):
    curr = region.token
    for step in path.steps:
        curr = step.apply(curr)
        if curr is None:
            return None
        curr = curr[0] if len(curr) == 1 else None
        if curr is None:
            return None
    return JsonRegion.create(curr)


def select_sequence(region, path):
    result = [region.token]
    for step in path.steps:
        result = select_many(step.apply(r) for r in result)
    return [JsonRegion.create(x) for x in result]
