import functools
import itertools
import json
# schemas

class SchemaElement(object):
    __slots__ = ['name', 'is_nullable', 'use_output', 'descendant_output_fields']

    def __init__(self, name, is_nullable, use_output):
        self.name = name
        self.is_nullable = is_nullable
        self.use_output = use_output
        self.descendant_output_fields = None

    def get_descendant_output_fields(self):
        raise NotImplementedError('Abstract method get_descendant_output_fields() not implemented in type: ' +
                                  str(type(self)))


class StructElement(SchemaElement):
    __slots__ = ['children']

    def __init__(self, name, is_nullable, use_output=True, children=None):
        super().__init__(name, is_nullable, use_output)
        self.children = children

    def get_descendant_output_fields(self):
        if self.descendant_output_fields is None:
            if self.children is None:
                all_descendants = []
            else:
                all_descendants = functools.reduce(list.__add__,
                                                   [c.get_descendant_output_fields() for c in self.children],
                                                   [])
            self.descendant_output_fields = all_descendants if not self.use_output else [self.name] + all_descendants
        return self.descendant_output_fields


class SequenceElement(SchemaElement):
    __slots__ = ['child']

    def __init__(self, name, is_nullable, use_output, child):
        super().__init__(name, is_nullable, use_output)
        self.child = child

    def get_descendant_output_fields(self):
        if self.descendant_output_fields is None:
            child_descendants = self.child.get_descendant_output_fields()
            if self.use_output:
                self.descendant_output_fields = [self.name] + child_descendants
            else:
                self.descendant_output_fields = child_descendants
        return self.descendant_output_fields


class TreeOutput(object):
    __slots__ = ['name']

    def __init__(self, name):
        self.name = name

    def to_table(self, schema, tree_to_table_semantics):
        raise NotImplementedError('Abstract method to_table() not implemented in type: ' + str(type(self)))

    def to_table_values_only(self, schema, tree_to_table_semantics):
        raise NotImplementedError('Abstract method to_table_values_only() not implemented in type: ' + str(type(self)))


class FieldOutput(TreeOutput):
    __slots__ = ['value', 'children']

    def __init__(self, name, value, children=None):
        super().__init__(name)
        self.value = value
        self.children = children

    def to_table(self, schema, tree_to_table_semantics):
        if self.children is None:
            return [[]] if self.name is None or self.name == '' else [[(self.name, self.value)]]

        if not isinstance(schema, StructElement):
            raise TypeError('Expected StructElement as top level schema in FieldOutput.to_table()')
        if len(self.children) != len(schema.children):
            raise TypeError('Number of children in schema does not match number of children in FieldOutput object')

        table = [[]]
        for child, schema_for_child in zip(self.children, schema.children):
            sub_table = child.to_table(schema_for_child, tree_to_table_semantics)
            table = (RepeatableEnumerable(itertools.chain, l, r)
                     for l, r in RepeatableEnumerable(itertools.product, table, sub_table))
        return table

    def to_table_values_only(self, schema, tree_to_table_semantics):
        if self.children is None:
            return [[]] if self.name is None or self.name == '' else \
                [[self.value.get_value() if self.value is not None else None]]

        if not isinstance(schema, StructElement):
            raise TypeError('Expected StructElement as top level schema in FieldOutput.to_table()')
        if len(self.children) != len(schema.children):
            raise TypeError('Number of children in schema does not match number of children in FieldOutput object')

        table = [[]]
        for child, schema_for_child in zip(self.children, schema.children):
            sub_table = child.to_table_values_only(schema_for_child, tree_to_table_semantics)
            table = (RepeatableEnumerable(itertools.chain, l, r)
                     for l, r in RepeatableEnumerable(itertools.product, table, sub_table))
        return table


class SequenceOutput(TreeOutput):
    __slots__ = ['elements']

    def __init__(self, name, elements):
        super().__init__(name)
        self.elements = elements

    def _to_table(self, schema, tree_to_table_semantics):
        if not isinstance(schema, SequenceElement):
            raise TypeError('Expected SequenceElement as top level schema in SequenceOutput.to_table()')

        yielded_one = False

        for e in self.elements:
            for v in e.to_table(schema.child, tree_to_table_semantics):
                yield v
                yielded_one = True

        if tree_to_table_semantics != 'OuterJoin' or yielded_one:
            return

        yield [(field, None) for field in schema.get_descendant_output_fields()]

    def _to_table_values_only(self, schema, tree_to_table_semantics):
        if not isinstance(schema, SequenceElement):
            raise TypeError('Expected SequenceElement as top level schema in SequenceOutput.to_table()')

        yielded_one = False

        for e in self.elements:
            for v in e.to_table_values_only(schema.child, tree_to_table_semantics):
                yield v
                yielded_one = True

        if tree_to_table_semantics != 'OuterJoin' or yielded_one:
            return

        yield [None] * len(schema.get_descendant_output_fields())

    def to_table(self, schema, tree_to_table_semantics):
        return RepeatableEnumerable(self._to_table, schema, tree_to_table_semantics)

    def to_table_values_only(self, schema, tree_to_table_semantics):
        return RepeatableEnumerable(self._to_table_values_only, schema, tree_to_table_semantics)
