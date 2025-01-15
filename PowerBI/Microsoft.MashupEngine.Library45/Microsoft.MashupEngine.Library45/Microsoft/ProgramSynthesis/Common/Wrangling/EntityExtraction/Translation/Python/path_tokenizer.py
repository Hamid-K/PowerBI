class PathToken(EntityToken):
    class_id = EntityType.Path

    def __eq__(self, other):
        return self._value_based_equality(other)

    def __hash__(self):
        return self._value_based_hash()


class FileNameToken(EntityToken):
    class_id = EntityType.FileName

    def __eq__(self, other):
        return self._value_based_equality(other)

    def __hash__(self):
        return self._value_based_hash()


class PathTokenizer(RegexBasedTokenizer):
    """
    >>> t = PathTokenizer()
    >>> data = r"sasf c:\windows\system32 dgdg"
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    1
    >>> str(path_tokens[0])
    'c:\\\\windows\\\\system32'
    >>> str(filename_tokens[0])
    'system32'

    >>> data = r"sasf c:\windows\system32\ dgdg"
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    0
    >>> str(path_tokens[0])
    'c:\\\\windows\\\\system32\\\\'

    >>> data = 'Random text with a "C:\\Program Files\\Office\\Excel.exe" Windows style path'
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    1
    >>> str(path_tokens[0])
    '"C:\\\\Program Files\\\\Office\\\\Excel.exe"'
    >>> str(filename_tokens[0])
    'Excel.exe'

    >>> data = r"sasf windows\system32 dgdg"
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    1
    >>> str(path_tokens[0])
    'windows\\\\system32'
    >>> str(filename_tokens[0])
    'system32'

    >>> data = r"sasf windows\system32\ dgdg"
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    0
    >>> str(path_tokens[0])
    'windows\\\\system32\\\\'

    >>> data = r'Random text with a "Program Files\\Office\\Excel.exe" Windows style path'
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    1
    >>> str(path_tokens[0])
    '"Program Files\\\\Office\\\\Excel.exe"'
    >>> str(filename_tokens[0])
    'Excel.exe'

    >>> data = r"foo and bar /usr/bin/ls and some other noise"
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    1
    >>> str(path_tokens[0])
    '/usr/bin/ls'
    >>> str(filename_tokens[0])
    'ls'

    >>> data = r"foo and bar /usr/bin/ls/ and some other noise"
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    0
    >>> str(path_tokens[0])
    '/usr/bin/ls/'

    >>> data = 'foo and bar "/windows/C/Program Files" and some other noise'
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    1
    >>> str(path_tokens[0])
    '"/windows/C/Program Files"'
    >>> str(filename_tokens[0])
    'Program Files'

    >>> data = 'foo and bar "/windows/C/Program Files/" and some other noise'
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    0
    >>> str(path_tokens[0])
    '"/windows/C/Program Files/"'

    >>> data = 'foo and bar "/windows/C/Program Files/Office/Excel.exe" and some other noise'
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    1
    >>> str(path_tokens[0])
    '"/windows/C/Program Files/Office/Excel.exe"'
    >>> str(filename_tokens[0])
    'Excel.exe'

    >>> data = 'foo and bar ""windows/C/Program Files"" and some other noise'
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    1
    >>> str(path_tokens[0])
    '"windows/C/Program Files"'
    >>> str(filename_tokens[0])
    'Program Files'

    >>> data = 'foo and bar ""windows/C/Program Files/"" and some other noise'
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    0
    >>> str(path_tokens[0])
    '"windows/C/Program Files/"'

    >>> data = 'foo and bar ""windows/C/Program Files/Office/Excel.exe"" and some other noise'
    >>> tokens = list(t.match_tokens(data))
    >>> path_tokens = list(filter(lambda t1: type(t1) == PathToken, tokens))
    >>> filename_tokens = list(filter(lambda t1: type(t1) == FileNameToken, tokens))
    >>> len(path_tokens)
    1
    >>> len(filename_tokens)
    1
    >>> str(path_tokens[0])
    '"windows/C/Program Files/Office/Excel.exe"'
    >>> str(filename_tokens[0])
    'Excel.exe'
    """
    path_component_group_name = "PathComponent"
    drive_letter_pattern_string = "\p{L}+\:"
    path_component_with_whitespace_pattern_string = \
        r"(?<{0}>(?:[\p{{L}}\p{{N}}\.\s_-])+)".format(path_component_group_name)
    path_component_without_whitespace_pattern_string = \
        r"(?<{0}>(?:[\p{{L}}\p{{N}}\._-])+)".format(path_component_group_name)
    windows_path_separator_pattern_string = r"\\"
    windows_path_separator = "\\"
    unix_path_separator_pattern_string = "/"
    open_quote_pattern_string = '(?:\\p{Pi}|")'
    close_quote_pattern_string = '(?:\\p{Pf}|")'

    unquoted_windows_left_context_pattern_string = \
        r'(?:^|[^\d\p{{L}}{0}"\p{{Pi}}])'.format(windows_path_separator_pattern_string)
    unquoted_windows_right_context_pattern_string = \
        r'(?:$|[^\d\p{{L}}{0}"\p{{Pf}}])'.format(windows_path_separator_pattern_string)
    unquoted_unix_left_context_pattern_string = \
        r'(?:^|[^\d\p{{L}}{0}"\p{{Pi}}])'.format(unix_path_separator_pattern_string)
    unquoted_unix_right_context_pattern_string = \
        r'(?:$|[^\d{0}"\p{{Pf}}])'.format(unix_path_separator_pattern_string)

    quoted_windows_left_context_pattern_string = \
        r'(?:^|[^\d\p{{L}}{0}])'.format(windows_path_separator_pattern_string)
    quoted_windows_right_context_pattern_string = \
        r'(?:$|[^\d{0}])'.format(windows_path_separator_pattern_string)
    quoted_unix_left_context_pattern_string = \
        r'(?:^|[^\d\p{{L}}{0}])'.format(unix_path_separator_pattern_string)
    quoted_unix_right_context_pattern_string = \
        r'(?:$|[^\d\p{{L}}{0}])'.format(unix_path_separator_pattern_string)

    windows_absolute_path_pattern_string = r"({0}(?:{1}(?:{2}))+{3}?)".format(
        drive_letter_pattern_string,
        windows_path_separator_pattern_string,
        path_component_without_whitespace_pattern_string,
        windows_path_separator_pattern_string
    )

    windows_relative_path_pattern_string = r"({0}(?:{1}{2})+{3}?)".format(
        path_component_without_whitespace_pattern_string,
        windows_path_separator_pattern_string,
        path_component_without_whitespace_pattern_string,
        windows_path_separator_pattern_string
    )

    unix_absolute_path_pattern_string = r"(?:(?:{0}{1})+{2}?)".format(
        unix_path_separator_pattern_string,
        path_component_without_whitespace_pattern_string,
        unix_path_separator_pattern_string
    )

    unix_relative_path_pattern_string = r"(?:{0}(?:{1}{2})+{3}?)".format(
        path_component_without_whitespace_pattern_string,
        unix_path_separator_pattern_string,
        path_component_without_whitespace_pattern_string,
        unix_path_separator_pattern_string
    )

    quoted_windows_absolute_path_pattern_string = r"{0}({1}(?:{2}(?:{3}))+{4}?){5}".format(
        open_quote_pattern_string,
        drive_letter_pattern_string,
        windows_path_separator_pattern_string,
        path_component_with_whitespace_pattern_string,
        windows_path_separator_pattern_string,
        close_quote_pattern_string
    )

    quoted_windows_relative_path_pattern_string = r"{0}({1}(?:{2}{3})+{4}?){5}".format(
        open_quote_pattern_string,
        path_component_with_whitespace_pattern_string,
        windows_path_separator_pattern_string,
        path_component_with_whitespace_pattern_string,
        windows_path_separator_pattern_string,
        close_quote_pattern_string
    )

    quoted_unix_absolute_path_pattern_string = r"{0}(?:(?:{1}{2})+{3}?){4}".format(
        open_quote_pattern_string,
        unix_path_separator_pattern_string,
        path_component_with_whitespace_pattern_string,
        unix_path_separator_pattern_string,
        close_quote_pattern_string
    )

    quoted_unix_relative_path_pattern_string = r"{0}(?:{1}(?:{2}{3})+{4}?){5}".format(
        open_quote_pattern_string,
        path_component_with_whitespace_pattern_string,
        unix_path_separator_pattern_string,
        path_component_with_whitespace_pattern_string,
        unix_path_separator_pattern_string,
        close_quote_pattern_string
    )

    def __init__(self):
        super().__init__(OverlapStrategy.SUBSUMPTION,
                         TokenPattern(PathTokenizer.windows_absolute_path_pattern_string,
                                      PathTokenizer.unquoted_windows_left_context_pattern_string,
                                      PathTokenizer.unquoted_windows_right_context_pattern_string),
                         TokenPattern(PathTokenizer.windows_relative_path_pattern_string,
                                      PathTokenizer.unquoted_windows_left_context_pattern_string,
                                      PathTokenizer.unquoted_windows_right_context_pattern_string),
                         TokenPattern(PathTokenizer.unix_absolute_path_pattern_string,
                                      PathTokenizer.unquoted_unix_left_context_pattern_string,
                                      PathTokenizer.unquoted_unix_right_context_pattern_string),
                         TokenPattern(PathTokenizer.unix_relative_path_pattern_string,
                                      PathTokenizer.unquoted_unix_left_context_pattern_string,
                                      PathTokenizer.unquoted_unix_right_context_pattern_string),
                         TokenPattern(PathTokenizer.quoted_windows_absolute_path_pattern_string,
                                      PathTokenizer.quoted_windows_left_context_pattern_string,
                                      PathTokenizer.quoted_windows_right_context_pattern_string),
                         TokenPattern(PathTokenizer.quoted_windows_relative_path_pattern_string,
                                      PathTokenizer.quoted_windows_left_context_pattern_string,
                                      PathTokenizer.quoted_windows_right_context_pattern_string),
                         TokenPattern(PathTokenizer.quoted_unix_absolute_path_pattern_string,
                                      PathTokenizer.quoted_unix_left_context_pattern_string,
                                      PathTokenizer.quoted_unix_right_context_pattern_string),
                         TokenPattern(PathTokenizer.quoted_unix_relative_path_pattern_string,
                                      PathTokenizer.quoted_unix_left_context_pattern_string,
                                      PathTokenizer.quoted_unix_right_context_pattern_string))

    def process_matches(self, matches):
        for m in matches:
            parts = list(filter(lambda y: y is not None,
                                (RegexBasedTokenizer.try_parse_int(x) for x in m.value().split('/'))))
            if (len(parts) == 3 and len(list(filter(lambda x: x <= 31, parts))) >= 2 and
                    len(list(filter(lambda x: x <= 12, parts))) >= 1 and all((x < 3000 for x in parts))):
                return

            yield PathToken(m.source, m.start, m.end)
            path_component_spans = RegexBasedTokenizer.get_group_spans(m.full_match,
                                                                       PathTokenizer.path_component_group_name)
            matched_value = m.value()
            matched_value = matched_value[1:len(matched_value) - 1] if matched_value.startswith('"') else matched_value
            if ((path_component_spans is not None) and (len(path_component_spans) > 0) and
                    (not matched_value.endswith(PathTokenizer.unix_path_separator_pattern_string)) and
                    (not matched_value.endswith(PathTokenizer.windows_path_separator))):
                last_span = path_component_spans[len(path_component_spans) - 1]
                last_capture = m.source[last_span[0]:last_span[1]]
                if len(last_capture) == 0:
                    return

                yield FileNameToken(m.source, last_span[0], last_span[1])
