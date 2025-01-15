using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Azure.Core.Json
{
	// Token: 0x020000BD RID: 189
	[NullableContext(1)]
	[Nullable(0)]
	internal struct MutableJsonChange
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00011E18 File Offset: 0x00010018
		[NullableContext(2)]
		public MutableJsonChange([Nullable(1)] string path, int index, object value, MutableJsonChangeKind changeKind, string addedPropertyName)
		{
			this.Path = path;
			this.Index = index;
			this.Value = value;
			this.ChangeKind = changeKind;
			this.AddedPropertyName = addedPropertyName;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00011E3F File Offset: 0x0001003F
		public readonly string Path { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00011E47 File Offset: 0x00010047
		public readonly int Index { get; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00011E4F File Offset: 0x0001004F
		[Nullable(2)]
		public readonly object Value
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00011E57 File Offset: 0x00010057
		[Nullable(2)]
		public readonly string AddedPropertyName
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00011E5F File Offset: 0x0001005F
		public readonly MutableJsonChangeKind ChangeKind { get; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00011E68 File Offset: 0x00010068
		public readonly JsonValueKind ValueKind
		{
			get
			{
				object value = this.Value;
				JsonValueKind jsonValueKind;
				if (value != null)
				{
					if (value is bool)
					{
						bool flag = (bool)value;
						jsonValueKind = (flag ? 5 : 6);
					}
					else if (!(value is string))
					{
						if (!(value is DateTime))
						{
							if (!(value is DateTimeOffset))
							{
								if (!(value is Guid))
								{
									if (!(value is byte))
									{
										if (!(value is sbyte))
										{
											if (!(value is short))
											{
												if (!(value is ushort))
												{
													if (!(value is int))
													{
														if (!(value is uint))
														{
															if (!(value is long))
															{
																if (!(value is ulong))
																{
																	if (!(value is float))
																	{
																		if (!(value is double))
																		{
																			if (!(value is decimal))
																			{
																				if (!(value is JsonElement))
																				{
																					throw new InvalidOperationException(string.Format("Unrecognized change type '{0}'.", this.Value.GetType()));
																				}
																				jsonValueKind = ((JsonElement)value).ValueKind;
																			}
																			else
																			{
																				jsonValueKind = 4;
																			}
																		}
																		else
																		{
																			jsonValueKind = 4;
																		}
																	}
																	else
																	{
																		jsonValueKind = 4;
																	}
																}
																else
																{
																	jsonValueKind = 4;
																}
															}
															else
															{
																jsonValueKind = 4;
															}
														}
														else
														{
															jsonValueKind = 4;
														}
													}
													else
													{
														jsonValueKind = 4;
													}
												}
												else
												{
													jsonValueKind = 4;
												}
											}
											else
											{
												jsonValueKind = 4;
											}
										}
										else
										{
											jsonValueKind = 4;
										}
									}
									else
									{
										jsonValueKind = 4;
									}
								}
								else
								{
									jsonValueKind = 3;
								}
							}
							else
							{
								jsonValueKind = 3;
							}
						}
						else
						{
							jsonValueKind = 3;
						}
					}
					else
					{
						jsonValueKind = 3;
					}
				}
				else
				{
					jsonValueKind = 7;
				}
				return jsonValueKind;
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00011F9B File Offset: 0x0001019B
		internal readonly void EnsureString()
		{
			if (this.ValueKind != 3)
			{
				throw new InvalidOperationException(string.Format("Expected a 'String' kind but was '{0}'.", this.ValueKind));
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00011FC1 File Offset: 0x000101C1
		internal readonly void EnsureNumber()
		{
			if (this.ValueKind != 4)
			{
				throw new InvalidOperationException(string.Format("Expected a 'Number' kind but was '{0}'.", this.ValueKind));
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00011FE7 File Offset: 0x000101E7
		internal readonly void EnsureArray()
		{
			if (this.ValueKind != 2)
			{
				throw new InvalidOperationException(string.Format("Expected an 'Array' kind but was '{0}'.", this.ValueKind));
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00012010 File Offset: 0x00010210
		internal readonly int GetArrayLength()
		{
			this.EnsureArray();
			object value = this.Value;
			if (value is JsonElement)
			{
				return ((JsonElement)value).GetArrayLength();
			}
			throw new InvalidOperationException(string.Format("Expected an 'Array' kind but was '{0}'.", this.ValueKind));
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001205B File Offset: 0x0001025B
		internal bool IsDescendant(string path)
		{
			return this.IsDescendant(MemoryExtensions.AsSpan(path));
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00012069 File Offset: 0x00010269
		[NullableContext(0)]
		internal bool IsDescendant(ReadOnlySpan<char> ancestorPath)
		{
			return MutableJsonChange.IsDescendant(ancestorPath, MemoryExtensions.AsSpan(this.Path));
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001207C File Offset: 0x0001027C
		[NullableContext(0)]
		internal unsafe static bool IsDescendant(ReadOnlySpan<char> ancestorPath, ReadOnlySpan<char> descendantPath)
		{
			if (ancestorPath.Length == 0)
			{
				return descendantPath.Length > 0;
			}
			return descendantPath.Length > ancestorPath.Length && MemoryExtensions.StartsWith<char>(descendantPath, ancestorPath) && *descendantPath[ancestorPath.Length] == 1;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000120CC File Offset: 0x000102CC
		internal bool IsDirectDescendant(string path)
		{
			if (!this.IsDescendant(path))
			{
				return false;
			}
			string[] array = path.Split(new char[] { '\u0001' });
			int num = (string.IsNullOrEmpty(array[0]) ? 0 : array.Length);
			int num2 = this.Path.Split(new char[] { '\u0001' }).Length;
			return num == num2 - 1;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00012124 File Offset: 0x00010324
		[NullableContext(0)]
		internal bool IsLessThan(ReadOnlySpan<char> otherPath)
		{
			return MemoryExtensions.SequenceCompareTo<char>(MemoryExtensions.AsSpan(this.Path), otherPath) < 0;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001213A File Offset: 0x0001033A
		[NullableContext(0)]
		internal bool IsGreaterThan(ReadOnlySpan<char> otherPath)
		{
			return MemoryExtensions.SequenceCompareTo<char>(MemoryExtensions.AsSpan(this.Path), otherPath) > 0;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00012150 File Offset: 0x00010350
		internal string AsString()
		{
			if (this.Value == null)
			{
				return "null";
			}
			return this.Value.ToString();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001216B File Offset: 0x0001036B
		public override string ToString()
		{
			return string.Format("Path={0}; Value={1}; Kind={2}; ChangeKind={3}", new object[] { this.Path, this.Value, this.ValueKind, this.ChangeKind });
		}
	}
}
