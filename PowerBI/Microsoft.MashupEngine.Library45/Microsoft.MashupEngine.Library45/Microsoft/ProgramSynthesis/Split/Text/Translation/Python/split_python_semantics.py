import functools
import regex


class MatchRecord(object):
    __slots__ = ['start_indices', 'end_indices', 'hash_code']

    def __init__(self, start_indices, end_indices):
        self.start_indices = start_indices
        self.end_indices = end_indices
        self.hash_code = None

    def __hash__(self):
        if self.hash_code is None:
            self.hash_code = functools.reduce(lambda acc, x: acc ^ x, self.start_indices + self.end_indices, 0)
        return self.hash_code

    def __eq__(self, other):
        if not isinstance(other, MatchRecord):
            return False
        if hash(self) != hash(other):
            return False
        return self.start_indices == other.start_indices and self.end_indices == other.end_indices

    def __ne__(self, other):
        return not (self == other)

    @staticmethod
    def _disjoint_union_list(m1, m2):
        result = []
        required_match_ordering = None

        if len(m1) != len(m2):
            return None

        for i in range(len(m1)):
            union_record, ordering = MatchRecord.disjoint_union(m1[i], m2[i])
            if union_record is None:
                return None
            if required_match_ordering is None:
                required_match_ordering = ordering
            elif ordering != required_match_ordering:
                return None
            result.append(union_record)
        return result

    @staticmethod
    def disjoint_union(m1, m2):
        if isinstance(m1, list) and isinstance(m2, list):
            return MatchRecord._disjoint_union_list(m1, m2)
        i1 = 0
        i2 = 0
        result_num_matches = len(m1.start_indices) + len(m2.start_indices)
        result_start_indexes = []
        result_end_indexes = []
        match_ordering = []
        for k in range(result_num_matches):    
            if (i1 == len(m1.start_indices)):
                result_start_indexes.append(m2.start_indices[i2])
                result_end_indexes.append(m2.end_indices[i2])
                match_ordering.append(1)
                i2 += 1
            elif (i2 == len(m2.start_indices)):
                result_start_indexes.append(m1.start_indices[i1])
                result_end_indexes.append(m1.end_indices[i1])
                match_ordering.append(0)
                i1 += 1
            else: 
                m1_start = m1.start_indices[i1]
                m1_end = m1.end_indices[i1]
                m2_start = m2.start_indices[i2]
                m2_end = m2.end_indices[i2]
                if (m1_start == m2_start):
                    if (m1_start == m1_end and m2_start != m2_end):
                        result_start_indexes.append(m1_start)
                        result_end_indexes.append(m1_end)
                        match_ordering.append(0)
                        i1 += 1
                    elif (m1_start != m1_end and m2_start == m2_end):
                        result_start_indexes.append(m2_start)
                        result_end_indexes.append(m2_end)
                        match_ordering.append(1)
                        i2 += 1
                    else:
                        return None, None
                elif (m1_start < m2_start):
                    if (m2_start < m1_end):
                        return None, None
                    result_start_indexes.append(m1_start)
                    result_end_indexes.append(m1_end)
                    match_ordering.append(0)
                    i1 += 1
                else:
                    if (m1_start < m2_end):
                        return None, None
                    result_start_indexes.append(m2_start)
                    result_end_indexes.append(m2_end)
                    match_ordering.append(1)
                    i2 += 1
        return MatchRecord(result_start_indexes, result_end_indexes), match_ordering

    def clone(self):
        return MatchRecord(list(self.start_indices), list(self.end_indices))


class SplitCell(object):
    __slots__ = ['value', 'isDelimiter']

    def __init__(self, value, isDelimiter):
        self.value = value
        self.isDelimiter = isDelimiter

def split_fixed_width(v, field_start_positions): 
    if field_start_positions is None:
        return MatchRecord([], [])
    sorted_positions = sorted(field_start_positions)    
    for i in range(len(sorted_positions)):
        p = sorted_positions[i]
        if (p < 0 or p > len(v.get_value())):
            return MatchRecord([], [])
    return MatchRecord(sorted_positions, sorted_positions)

def split_fixed_width_delimiters(v, delimiter_positions):
    if delimiter_positions is None:
        return MatchRecord([], [])
    start_positions = []
    end_positions = []
    length = len(v.get_value())
    for i in range(len(delimiter_positions)):
        start = delimiter_positions[i][0]
        end = delimiter_positions[i][1]
        if start < 0 or end > length or start > end:
            return MatchRecord([], [])
        start_positions.append(start)
        end_positions.append(end)
    return MatchRecord(start_positions, end_positions)

def _split_get_match_record(region, regexp):
    if regexp is None:
        return MatchRecord([], [])
    if isinstance(regexp, RegularExpression):
        matches = region.match(regexp)
        if (len(matches) == 0):
            return MatchRecord([],[])
        starts, ends = zip(*matches)
        return MatchRecord(starts, ends)
    elif isinstance(regexp, str):
        return _split_get_match_record(region, RegularExpression([regexp]))
    else:
        return None
    
def split_regex_match(region, regexp):
    return _split_get_match_record(region, regexp)

def split_field_match(region, regexp):
    return _split_get_match_record(region, regexp)

def split_concat(t, r):
    start_indices = []
    end_indices = []
    i1 = 0
    i2 = 0
    while (i1 < len(t.start_indices)):
        t_start = t.start_indices[i1]
        t_end = t.end_indices[i1]
        while (i2 < len(r.start_indices) and r.start_indices[i2] < t_end):
            i2 += 1
        if (i2 == len(r.start_indices)):
            break
        if (r.start_indices[i2] == t_end):
            start_indices.append(t_start)
            end_indices.append(r.end_indices[i2])
        i1 += 1
    return MatchRecord(start_indices, end_indices)


_split_start_regex = RegularExpression(['^'])
_split_end_regex = RegularExpression(['$'])
_split_numeric_regex = RegularExpression(['\\d+'])
_split_digit_regex = RegularExpression(['\\d'])
_split_upper_case_regex = RegularExpression(['[A-Z]+'])
_split_lower_case_regex = RegularExpression(['[a-z]+'])
_split_alphabets_regex = RegularExpression(['[A-Za-z]+'])
_split_alpha_numeric_regex = RegularExpression(['[A-Za-z0-9]+'])
_split_white_space_regex = RegularExpression(['\\s+'])
_split_all_chars_regex = RegularExpression(['.+'])


def split_empty(region):
    allPositions = list(range(len(region.content)+1))
    return MatchRecord(allPositions, allPositions)


def split_const_str(region, s):
    escaped_regex = regex.escape(s) 
    return _split_get_match_record(region, escaped_regex)

def split_const_str_with_whitespace(region, s):
    escaped_regex = '\\s*' + regex.escape(s) + '\\s*'
    return _split_get_match_record(region, escaped_regex)

def split_const_alph_str(region, a):
    maximal_match_with_whitespace = '\\s*(?<![A-Za-z])' + regex.escape(a) + '(?![A-Za-z])\\s*'
    return _split_get_match_record(region, maximal_match_with_whitespace)

def split_constant_delimiter(region, s):
    escaped_regex = regex.escape(s) 
    if s is None or escaped_regex is None:
        return MatchRecord([], [])
    r = RegularExpression([escaped_regex])
    matches = region.match(r)
    if (len(matches) == 0):
        return MatchRecord([],[])
    starts, ends = zip(*matches)
    filtered_starts = []
    filtered_ends = []
    quotedRegions = get_quoted_regions(region.content)
    for i in range(len(starts)): 
        last_position = max(starts[i], ends[i] - 1) 
        if ((not in_quoted_region(quotedRegions, starts[i])) and not in_quoted_region(quotedRegions, last_position)):
            filtered_starts.append(starts[i])
            filtered_ends.append(ends[i])
    result = MatchRecord(filtered_starts, filtered_ends)
    return result

def in_quoted_region(quotedRegions, position, lowIndex = -1, highIndex = -1):
    if (lowIndex == -1):
        return in_quoted_region(quotedRegions, position, 0, len(quotedRegions) - 1)
    if (len(quotedRegions) == 0 or highIndex < lowIndex):
        return False
    difference = highIndex - lowIndex
    if (difference <= 1):
        if (quotedRegions[lowIndex][0] <= position and quotedRegions[lowIndex][1] >= position):
            return True
        if (highIndex > lowIndex and quotedRegions[highIndex][0] <= position and quotedRegions[highIndex][1] >= position):
            return True
        return False
    mid_index = lowIndex + (difference // 2)
    if (quotedRegions[mid_index][0] > position):
        return in_quoted_region(quotedRegions, position, lowIndex, mid_index - 1)
    return in_quoted_region(quotedRegions, position, mid_index, highIndex)
        
def get_quoted_regions(s):
    result = []
    inside_quotes = False
    region_start = -1
    region_end = -1
    i = 0
    while (i < len(s)):
        if (s[i] == '"'):
            if (inside_quotes):
                next = i + 1
                if (next < len(s) and s[next] == '"'):
                    i = i + 1
                else:
                    inside_quotes = False
                    region_end = i
                    result.append((region_start, region_end))
            else:
                inside_quotes = True
                region_start = i
        i = i + 1
    return result

def _split_look_behind(r):
	return MatchRecord(list(r.end_indices), list(r.end_indices))
    
def _split_look_ahead(r):
	return MatchRecord(list(r.start_indices), list(r.start_indices))

def split_look_around(r1, delimiter_matches, r2):
    return split_concat(split_concat(_split_look_behind(r1), delimiter_matches), _split_look_ahead(r2))

def split_field_end_points(field_matches): 
    return get_end_points_record(field_matches)

def split_field_look_around_end_points(r1, field_matches, r2):
    m = split_concat(split_concat(_split_look_behind(r1), field_matches), _split_look_ahead(r2))
    return get_end_points_record(m)

def get_end_points_record(m):
    delimiting_points = sorted(set(m.start_indices + m.end_indices))
    return MatchRecord(delimiting_points, delimiting_points)

def split_split_multiple(splitting, d):
    u, o = MatchRecord.disjoint_union(splitting, d)
    if u is None:
        return MatchRecord([],[])
    return u

def split_delimiters_list(delimiter_list, d):
    return delimiter_list + [d]

def split_empty_delimiters_list():
    return []

def split_ext_points_list(extraction_points, cnd_ext_point): 
    return extraction_points + [cnd_ext_point]
            
def split_empty_ext_points_list():
    return []
      
def split_special_char_pattern(v, pattern):
    s = v.get_value()
    i = 0
    for c in s: 
        if not (c.isdigit() or c.isalpha() or c.isspace()):
            if (i >= len(pattern)):
                return False
            if (pattern[i] != c):
                return False
            i = i + 1
    return True
    
def split_conditional_extract(pred, ext_point, cnd_ext_point):
    if (pred):
        return ext_point
    return cnd_ext_point
        
def split_extraction_split(v, delimiters_list, extraction_points):
    result = [None] * len(extraction_points)
    for i in range(len(extraction_points)):
        e = extraction_points[i]
        if (e is None):
            return None
        if (e[0] == -2):
            result[i] = SplitCell(None, False)
            continue
        start_index = 0
        end_index = 0
        if (e[0] >= 0):
            left_delimiter = delimiters_list[e[0]]
            left_occurrence = e[1] if e[1] >= 0 else len(left_delimiter.start_indices) + e[1]
            if (left_occurrence < 0 or left_occurrence >= len(left_delimiter.start_indices)):
                result[i] = SplitCell(None, False)
                continue
            start_index = left_delimiter.end_indices[left_occurrence]
        else:
            start_index = 0
        if (e[2] >= 0):
            right_delimiter = delimiters_list[e[2]]
            right_occurrence = e[3] if e[3] >= 0 else len(right_delimiter.start_indices) + e[3]
            if (right_occurrence < 0 or right_occurrence >= len(right_delimiter.start_indices)):
                result[i] = SplitCell(None, False)
                continue
            end_index = right_delimiter.start_indices[right_occurrence]
        else:
            end_index = v.end
        if (start_index > end_index):
            result[i] = SplitCell(None, False)
        else:
            result[i] = SplitCell(v.slice(start_index, end_index), False)
    return result

def split_split_region(v, match_record, ignore_indices, num_splits, delimiter_start, delimiter_end, include_delimiters, fill_strategy):
    keep_indices = list(filter(lambda x: x not in ignore_indices, range(len(match_record.start_indices))))
    result_start_indices = [match_record.start_indices[y] for y in keep_indices]
    result_end_indices = [match_record.end_indices[y] for y in keep_indices]
    split_cells = []
    last_end_position = 0
    conforming_input = True;
    for i in range(len(result_start_indices)):
        start = result_start_indices[i]
        end = result_end_indices[i]
        if (not (delimiter_start and i == 0 and start == last_end_position)):
            split_cells.append(SplitCell(v.slice(last_end_position, start), False))
            if (delimiter_start and i == 0):
                conforming_input = False  
        if (include_delimiters and start != end):  
            split_cells.append(SplitCell(v.slice(start, end), True))
        last_end_position = end
    if (not (delimiter_end and v.end == last_end_position)):
        split_cells.append(SplitCell(v.slice(last_end_position, v.end), False))
        if (delimiter_end):
            conforming_input = False
    conforming_input = conforming_input and len(split_cells) == num_splits 
    if (conforming_input):
        return split_cells
    result = [None] * num_splits
    if (fill_strategy == 0):
        for i in range(num_splits):
            if (i < len(split_cells)):
                result[i] = SplitCell(None,split_cells[i].isDelimiter)
            else:
                result[i] = SplitCell(None,False)
    elif (fill_strategy == 1): 
        for i in range(num_splits):
            if (i < len(split_cells)):
                if (i == num_splits - 1):
                    start = split_cells[i].value.start
                    end = split_cells[len(split_cells) - 1].value.end
                    result[i] = SplitCell(v.slice(start, end), split_cells[i].isDelimiter)
                else:
                    result[i] = split_cells[i]
            else:
                result[i] = SplitCell(None, False)
    elif (fill_strategy == 2):
        for i in range(num_splits):
            result_index = len(result) - (i + 1)
            if (i < len(split_cells)):
                split_cells_index = len(split_cells) - (i + 1)
                if (result_index == 0):
                    start = split_cells[0].value.start
                    end = split_cells[split_cells_index].value.end
                    result[result_index] = SplitCell(v.slice(start, end), split_cells[split_cells_index].isDelimiter)
                else:
                    result[result_index] = split_cells[split_cells_index]
            else:
                    result[result_index] = SplitCell(None, False)
    return result
