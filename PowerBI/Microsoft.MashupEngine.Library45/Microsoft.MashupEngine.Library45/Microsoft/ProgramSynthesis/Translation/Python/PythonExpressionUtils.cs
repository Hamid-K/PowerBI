using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x0200030C RID: 780
	public static class PythonExpressionUtils
	{
		// Token: 0x060010EC RID: 4332 RVA: 0x000309A9 File Offset: 0x0002EBA9
		public static SSALiteral MkLiteral(decimal k)
		{
			return new SSALiteral(PythonExpressionUtils.NumType, FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { k })));
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000309D3 File Offset: 0x0002EBD3
		public static SSALiteral MkLiteral(uint k)
		{
			return new SSALiteral(PythonExpressionUtils.NumType, FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { k })));
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000309FD File Offset: 0x0002EBFD
		public static SSALiteral MkLiteral(string s)
		{
			return new SSALiteral(PythonExpressionUtils.StrType, s);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00030A0A File Offset: 0x0002EC0A
		public static SSALiteral MkPyLiteral(string s)
		{
			return PythonExpressionUtils.MkLiteral(s.ToPythonLiteral());
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00030A17 File Offset: 0x0002EC17
		public static SSARValue MkFunApp(Type t, string name, params SSAValue[] args)
		{
			return new SSAFunctionApplication(t, name, args, true);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00030A22 File Offset: 0x0002EC22
		private static SSARValue RegexDot(SSAValue x)
		{
			return PythonExpressionUtils.MkFunApp(x.ValueType, "operators.__dot__", new SSAValue[]
			{
				PythonExpressionUtils.Regex,
				x
			});
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00030A46 File Offset: 0x0002EC46
		public static SSARValue RegexFindIter(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.RegexDot(PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "finditer", new SSAValue[] { x, y }));
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00030A6A File Offset: 0x0002EC6A
		public static SSARValue RegexMatch(params SSAValue[] x)
		{
			return PythonExpressionUtils.RegexDot(PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "match", x));
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00030A81 File Offset: 0x0002EC81
		public static SSARValue RegexFullMatch(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.RegexDot(PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ObjType, "fullmatch", new SSAValue[] { x, y }));
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00030AA5 File Offset: 0x0002ECA5
		public static SSARValue RegexSearch(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.RegexDot(PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ObjType, "search", new SSAValue[] { x, y }));
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00030AC9 File Offset: 0x0002ECC9
		public static SSARValue Group(int x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ObjType, "group", new SSAValue[] { PythonExpressionUtils.MkLiteral(x) });
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00030AEE File Offset: 0x0002ECEE
		public static SSARValue Next(SSAValue x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ObjType, "next", new SSAValue[] { x });
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00030B09 File Offset: 0x0002ED09
		public static SSARValue Enumerate(SSAValue x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "enumerate", new SSAValue[] { x });
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00030B24 File Offset: 0x0002ED24
		public static SSARValue Index(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "index", new SSAValue[] { y })
			});
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00030B5C File Offset: 0x0002ED5C
		public static SSARValue RIndex(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "rindex", new SSAValue[] { y })
			});
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00030B94 File Offset: 0x0002ED94
		public static SSARValue List(SSAValue x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "list", new SSAValue[] { x });
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00030BAF File Offset: 0x0002EDAF
		public static SSARValue Len(SSAValue x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "len", new SSAValue[] { x });
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00030BCA File Offset: 0x0002EDCA
		public static SSARValue Tuple(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "operators.__tuple__", new SSAValue[] { x, y });
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00030BE9 File Offset: 0x0002EDE9
		public static SSARValue GetItem(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ObjType, "operators.__getitem__", new SSAValue[] { x, y });
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00030C08 File Offset: 0x0002EE08
		public static SSARValue ForIn(SSAValue x, SSAValue y, SSAValue z)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "operators.__for_in_if__", new SSAValue[] { x, y, z });
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00030C2B File Offset: 0x0002EE2B
		public static SSARValue ForInIf(SSAValue x, SSAValue y, SSAValue z, SSAValue w)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "operators.__for_in_if__", new SSAValue[] { x, y, z, w });
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00030C52 File Offset: 0x0002EE52
		public static SSARValue MakeList(IEnumerable<string> x)
		{
			return PythonExpressionUtils.MakeList(x.Select((string y) => PythonExpressionUtils.MkPyLiteral(y)));
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00030C7E File Offset: 0x0002EE7E
		public static SSARValue MakeList(IEnumerable<SSAValue> x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "operators.__make_list__", x.ToArray<SSAValue>());
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00030C95 File Offset: 0x0002EE95
		public static SSARValue Filter(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "filter", new SSAValue[] { x, y });
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00030CB4 File Offset: 0x0002EEB4
		public static SSARValue DDecimal(SSAValue x)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				PythonExpressionUtils.Decimal,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "Decimal", new SSAValue[] { x })
			});
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00030CF0 File Offset: 0x0002EEF0
		public static SSARValue Quantize(SSAValue x, string y, string mode = "ROUND_HALF_UP")
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "quantize", new SSAValue[]
				{
					PythonExpressionUtils.DDecimal(y),
					PythonExpressionUtils.NamedArg("rounding", PythonExpressionUtils.Dot(new SSAValue[]
					{
						PythonExpressionUtils.Decimal,
						PythonExpressionUtils.MkLiteral(mode)
					}))
				})
			});
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00030D56 File Offset: 0x0002EF56
		public static SSARValue DDecimal(string x)
		{
			return PythonExpressionUtils.DDecimal(PythonExpressionUtils.MkLiteral(x));
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00030D63 File Offset: 0x0002EF63
		private static SSARValue MathDot(SSAValue x)
		{
			return PythonExpressionUtils.MkFunApp(x.ValueType, "operators.__dot__", new SSAValue[]
			{
				PythonExpressionUtils.Math,
				x
			});
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00030D87 File Offset: 0x0002EF87
		public static SSARValue Ceil(SSAValue x)
		{
			return PythonExpressionUtils.MathDot(PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ObjType, "ceil", new SSAValue[] { x }));
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00030DA8 File Offset: 0x0002EFA8
		public static SSARValue Strip(SSAValue x, string s = null)
		{
			if (s != null)
			{
				return PythonExpressionUtils.Dot(new SSAValue[]
				{
					x,
					PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "strip", new SSAValue[] { PythonExpressionUtils.MkLiteral(s) })
				});
			}
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "strip", Array.Empty<SSAValue>())
			});
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00030E10 File Offset: 0x0002F010
		public static SSARValue LStrip(SSAValue x, string s = null)
		{
			if (s != null)
			{
				return PythonExpressionUtils.Dot(new SSAValue[]
				{
					x,
					PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "lstrip", new SSAValue[] { PythonExpressionUtils.MkLiteral(s) })
				});
			}
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "lstrip", Array.Empty<SSAValue>())
			});
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00030E77 File Offset: 0x0002F077
		public static SSARValue Lower(SSAValue x)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "lower", Array.Empty<SSAValue>())
			});
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00030E9F File Offset: 0x0002F09F
		public static SSARValue Upper(SSAValue x)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "upper", Array.Empty<SSAValue>())
			});
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00030EC7 File Offset: 0x0002F0C7
		public static SSARValue Title(SSAValue x)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "title", Array.Empty<SSAValue>())
			});
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00030EF0 File Offset: 0x0002F0F0
		public static SSARValue Join(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "join", new SSAValue[] { y })
			});
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00030F28 File Offset: 0x0002F128
		public static SSARValue LJust(SSAValue x, uint y, char c)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "ljust", new SSAValue[]
				{
					PythonExpressionUtils.MkLiteral(y),
					PythonExpressionUtils.MkLiteral(FormattableString.Invariant(FormattableStringFactory.Create("'{0}'", new object[] { c })))
				})
			});
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00030F8C File Offset: 0x0002F18C
		public static SSARValue RJust(SSAValue x, uint y, char c)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "rjust", new SSAValue[]
				{
					PythonExpressionUtils.MkLiteral(y),
					PythonExpressionUtils.MkLiteral(FormattableString.Invariant(FormattableStringFactory.Create("'{0}'", new object[] { c })))
				})
			});
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00030FF0 File Offset: 0x0002F1F0
		public static SSARValue Split(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "split", new SSAValue[] { y })
			});
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00031028 File Offset: 0x0002F228
		public static SSARValue Split(SSAValue x, SSAValue y, SSAValue z)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "split", new SSAValue[] { y, z })
			});
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00031064 File Offset: 0x0002F264
		public static SSARValue RSplit(SSAValue x, SSAValue y, SSAValue z)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "rsplit", new SSAValue[] { y, z })
			});
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000310A0 File Offset: 0x0002F2A0
		public static SSARValue Format(string x, SSAValue y)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				PythonExpressionUtils.MkPyLiteral(x),
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "format", new SSAValue[] { y })
			});
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000310DD File Offset: 0x0002F2DD
		public static SSARValue Slice(SSAValue x, SSAValue y, SSAValue z)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "operators.__getitem_slice2__", new SSAValue[] { x, y, z });
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00031100 File Offset: 0x0002F300
		public static SSARValue SliceEndOnly(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "operators.__getitem_slice_end_only__", new SSAValue[] { x, y });
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00031120 File Offset: 0x0002F320
		public static SSARValue SubString(SSAValue x, int start = 0, int end = 0)
		{
			if (start != 0)
			{
				return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "operators.__getitem_slice_start_only__", new SSAValue[]
				{
					x,
					PythonExpressionUtils.MkLiteral(start)
				});
			}
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "operators.__getitem_slice_end_only__", new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkLiteral(end)
			});
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0003117F File Offset: 0x0002F37F
		public static SSARValue RSubStr(SSAValue x, SSAValue pos1)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "operators.__getitem_slice_start_only__", new SSAValue[] { x, pos1 });
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0003119E File Offset: 0x0002F39E
		public static SSARValue Replace(string x, string y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "replace", new SSAValue[]
			{
				PythonExpressionUtils.MkPyLiteral(x),
				PythonExpressionUtils.MkPyLiteral(y)
			});
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000311C7 File Offset: 0x0002F3C7
		public static SSARValue Replace(string x, string y, int z)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "replace", new SSAValue[]
			{
				PythonExpressionUtils.MkPyLiteral(x),
				PythonExpressionUtils.MkPyLiteral(y),
				PythonExpressionUtils.MkLiteral(z)
			});
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x000311FE File Offset: 0x0002F3FE
		public static SSARValue Str(SSAValue x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "str", new SSAValue[] { x });
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00031219 File Offset: 0x0002F419
		public static SSARValue And(params SSAValue[] x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "operators.__and__", x);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0003122B File Offset: 0x0002F42B
		public static SSARValue Or(params SSAValue[] x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "operators.__or__", x);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003123D File Offset: 0x0002F43D
		public static SSARValue NotIn(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "operators.__notin__", new SSAValue[] { x, y });
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0003125C File Offset: 0x0002F45C
		public static SSARValue Equals(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "operators.__equals__", new SSAValue[] { x, y });
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0003127B File Offset: 0x0002F47B
		public static SSARValue NotEquals(SSAValue x, string y)
		{
			return PythonExpressionUtils.NotEquals(x, PythonExpressionUtils.MkLiteral(y));
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00031289 File Offset: 0x0002F489
		public static SSARValue NotEquals(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "operators.__not_equals__", new SSAValue[] { x, y });
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000312A8 File Offset: 0x0002F4A8
		public static SSARValue LessEquals(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "operators.__lte__", new SSAValue[] { x, y });
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000312C8 File Offset: 0x0002F4C8
		public static SSARValue StartsWith(SSAValue x, string y, int start = 0)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				(start == 0) ? x : PythonExpressionUtils.SubString(x, start, 0),
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "startswith", new SSAValue[] { PythonExpressionUtils.MkPyLiteral(y) })
			});
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00031314 File Offset: 0x0002F514
		public static SSARValue EndsWith(SSAValue x, string y, int end = 0)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				(end == 0) ? x : PythonExpressionUtils.SubString(x, 0, end),
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "endswith", new SSAValue[] { PythonExpressionUtils.MkPyLiteral(y) })
			});
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0003135E File Offset: 0x0002F55E
		public static SSARValue IsInstance(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "isinstance", new SSAValue[] { x, y });
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0003137D File Offset: 0x0002F57D
		public static SSARValue IsInstanceStr(SSAValue x)
		{
			return PythonExpressionUtils.IsInstance(x, new SSALiteral(PythonExpressionUtils.ObjType, "str"));
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00031394 File Offset: 0x0002F594
		public static SSARValue Abs(SSAValue x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "abs", new SSAValue[] { x });
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000313AF File Offset: 0x0002F5AF
		public static SSARValue Add(params SSAValue[] x)
		{
			return PythonExpressionUtils.MkFunApp(x[0].ValueType, "operators.__add__", x);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000313C4 File Offset: 0x0002F5C4
		public static SSARValue Mod(SSAValue x, uint y)
		{
			return PythonExpressionUtils.Mod(x, PythonExpressionUtils.MkLiteral(y));
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000313D2 File Offset: 0x0002F5D2
		public static SSARValue Mod(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "operators.__mod__", new SSAValue[] { x, y });
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000313F1 File Offset: 0x0002F5F1
		public static SSARValue Minus(uint x, SSAValue y)
		{
			return PythonExpressionUtils.Minus(PythonExpressionUtils.MkLiteral(x), y);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000313FF File Offset: 0x0002F5FF
		public static SSARValue Minus(SSAValue x, int y)
		{
			return PythonExpressionUtils.Minus(x, PythonExpressionUtils.MkLiteral(y));
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00031412 File Offset: 0x0002F612
		public static SSARValue Minus(SSAValue x, decimal y)
		{
			return PythonExpressionUtils.Minus(x, PythonExpressionUtils.MkLiteral(y));
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00031420 File Offset: 0x0002F620
		public static SSARValue Minus(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "operators.__minus__", new SSAValue[] { x, y });
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0003143F File Offset: 0x0002F63F
		public static SSARValue Times(SSAValue x, decimal y)
		{
			return PythonExpressionUtils.Times(PythonExpressionUtils.MkLiteral(y), x);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0003144D File Offset: 0x0002F64D
		public static SSARValue Times(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "operators.__times__", new SSAValue[] { x, y });
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0003146C File Offset: 0x0002F66C
		public static SSARValue DivideBy(int x, decimal y)
		{
			return PythonExpressionUtils.DivideBy(PythonExpressionUtils.MkLiteral(x), PythonExpressionUtils.MkLiteral(y));
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00031484 File Offset: 0x0002F684
		public static SSARValue DivideBy(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "operators.__divideby__", new SSAValue[] { x, y });
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000314A3 File Offset: 0x0002F6A3
		public static SSARValue IntDivideBy(SSAValue x, decimal y)
		{
			return PythonExpressionUtils.IntDivideBy(x, PythonExpressionUtils.MkLiteral(y));
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000314B1 File Offset: 0x0002F6B1
		public static SSARValue IntDivideBy(SSAValue x, uint y)
		{
			return PythonExpressionUtils.IntDivideBy(x, PythonExpressionUtils.MkLiteral(y));
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000314BF File Offset: 0x0002F6BF
		public static SSARValue IntDivideBy(SSAValue x, SSAValue y)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "operators.__intdivideby__", new SSAValue[] { x, y });
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000314DE File Offset: 0x0002F6DE
		public static SSARValue Range(SSAValue x, int y, int z)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ListType, "range", new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkLiteral(y),
				PythonExpressionUtils.MkLiteral(z)
			});
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00031515 File Offset: 0x0002F715
		public static SSARValue Max(params SSAValue[] x)
		{
			return PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "max", x);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00031527 File Offset: 0x0002F727
		public static SSARValue Max0(SSAValue x)
		{
			return PythonExpressionUtils.Max(new SSAValue[]
			{
				x,
				PythonExpressionUtils.Zero
			});
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00031540 File Offset: 0x0002F740
		public static SSARValue Dot(params SSAValue[] x)
		{
			return PythonExpressionUtils.MkFunApp(x[x.Length - 1].ValueType, "operators.__dot__", x);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00031559 File Offset: 0x0002F759
		public static SSARValue NamedArg(string name, SSAValue a)
		{
			return PythonExpressionUtils.MkFunApp(a.ValueType, "operators.__assign__", new SSAValue[]
			{
				PythonExpressionUtils.MkLiteral(name),
				a
			});
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0003157E File Offset: 0x0002F77E
		public static SSARValue NamedArg(string name, uint a)
		{
			return PythonExpressionUtils.NamedArg(name, PythonExpressionUtils.MkLiteral(a));
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0003158C File Offset: 0x0002F78C
		public static SSARValue IfThenElse(SSAValue x, SSAValue y, SSAValue z)
		{
			return PythonExpressionUtils.MkFunApp(y.ValueType, "operators.__ite__", new SSAValue[] { x, y, z });
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000315B0 File Offset: 0x0002F7B0
		private static SSARValue DatetimeDot(SSAValue x)
		{
			return PythonExpressionUtils.MkFunApp(x.ValueType, "operators.__dot__", new SSAValue[]
			{
				PythonExpressionUtils.Datetime,
				x
			});
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000315D4 File Offset: 0x0002F7D4
		public static SSARValue Strftime(SSAValue x, string y)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.StrType, "strftime", new SSAValue[] { PythonExpressionUtils.MkLiteral(y) })
			});
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00031611 File Offset: 0x0002F811
		public static SSARValue TimeDelta(params SSAValue[] x)
		{
			return PythonExpressionUtils.DatetimeDot(PythonExpressionUtils.MkFunApp(PythonExpressionUtils.TimeType, "timedelta", x));
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00031628 File Offset: 0x0002F828
		public static SSARValue DateTime(params SSAValue[] x)
		{
			return PythonExpressionUtils.DatetimeDot(PythonExpressionUtils.MkFunApp(PythonExpressionUtils.DateType, "datetime", x));
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0003163F File Offset: 0x0002F83F
		public static SSARValue Strptime(SSAValue x, string y)
		{
			return PythonExpressionUtils.DatetimeDot(PythonExpressionUtils.DatetimeDot(PythonExpressionUtils.MkFunApp(PythonExpressionUtils.DateType, "strptime", new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkPyLiteral(y)
			})));
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00031670 File Offset: 0x0002F870
		private static bool ArgNeedsParen(string topF, string argF)
		{
			int num = 200;
			int num2;
			if (!PythonNameUtils.Operators.OpPrec.TryGetValue(topF, out num2))
			{
				num2 = num;
			}
			int num3;
			if (!PythonNameUtils.Operators.OpPrec.TryGetValue(argF, out num3))
			{
				num3 = num;
			}
			return num2 != num && (num2 > num3 || (num2 == num3 && (!(topF == argF) || !PythonNameUtils.Operators.AcLikeOps.Contains(topF))));
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000316D0 File Offset: 0x0002F8D0
		public static string ToPython(this SSAValue val, string className = "", HashSet<SSARegister> constantRegisters = null, bool useClass = true)
		{
			return PythonExpressionUtils.Translate(className, val, constantRegisters, useClass);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000316DC File Offset: 0x0002F8DC
		private static string Translate(string headerModuleName, SSAValue value, HashSet<SSARegister> constantRegisters = null, bool useClass = true)
		{
			string text = (useClass ? "self." : "");
			string text2 = "None";
			PythonExpressionUtils.<>c__DisplayClass104_1 CS$<>8__locals2 = new PythonExpressionUtils.<>c__DisplayClass104_1();
			SSAVariable ssavariable = value as SSAVariable;
			if (ssavariable != null)
			{
				return ssavariable.VariableName;
			}
			SSALiteral ssaliteral = value as SSALiteral;
			if (ssaliteral != null)
			{
				return ssaliteral.LiteralString;
			}
			SSARegister ssaregister = value as SSARegister;
			if (ssaregister != null)
			{
				if (useClass)
				{
					HashSet<SSARegister> constantRegisters2 = constantRegisters;
					if (constantRegisters2 != null && constantRegisters2.Contains(ssaregister))
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}", new object[]
						{
							text,
							ssaregister.GetName()
						}));
					}
				}
				return ssaregister.GetName();
			}
			CS$<>8__locals2.<asApp>5__2 = value as SSAFunctionApplication;
			if (CS$<>8__locals2.<asApp>5__2 == null)
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("SSAValue of type \"{0}\" isn't supported", new object[] { value.GetType() })));
			}
			CS$<>8__locals2.arguments = CS$<>8__locals2.<asApp>5__2.FunctionArguments.Select((SSAValue arg) => PythonExpressionUtils.Translate(headerModuleName, arg, constantRegisters, useClass)).ToList<string>();
			string functionName = CS$<>8__locals2.<asApp>5__2.FunctionName;
			if (functionName != null)
			{
				switch (functionName.Length)
				{
				case 16:
					if (functionName == "operators.__or__")
					{
						return string.Join(" or ", CS$<>8__locals2.arguments.Select((string x, int i) => base.<Translate>g__ArgMaybeWithParens|1(i)));
					}
					break;
				case 17:
				{
					char c = functionName[12];
					if (c != 'a')
					{
						if (c != 'd')
						{
							switch (c)
							{
							case 'i':
								if (functionName == "operators.__ite__")
								{
									return FormattableString.Invariant(FormattableStringFactory.Create("{0} if {1} else {2}", new object[]
									{
										CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1),
										CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
										CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(2)
									}));
								}
								break;
							case 'l':
								if (functionName == "operators.__lte__")
								{
									return FormattableString.Invariant(FormattableStringFactory.Create("{0} <= {1}", new object[]
									{
										CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
										CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
									}));
								}
								break;
							case 'm':
								if (functionName == "operators.__mod__")
								{
									return FormattableString.Invariant(FormattableStringFactory.Create("{0} % {1}", new object[]
									{
										CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
										CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
									}));
								}
								break;
							}
						}
						else if (functionName == "operators.__dot__")
						{
							return string.Join(".", CS$<>8__locals2.arguments.Select((string x, int i) => base.<Translate>g__ArgMaybeWithParens|1(i)));
						}
					}
					else
					{
						if (functionName == "operators.__add__")
						{
							return string.Join(" + ", CS$<>8__locals2.arguments.Select((string x, int i) => base.<Translate>g__ArgMaybeWithParens|1(i)));
						}
						if (functionName == "operators.__and__")
						{
							return string.Join(" and ", CS$<>8__locals2.arguments.Select((string x, int i) => base.<Translate>g__ArgMaybeWithParens|1(i)));
						}
					}
					break;
				}
				case 19:
				{
					char c = functionName[14];
					switch (c)
					{
					case 'm':
						if (functionName == "operators.__times__")
						{
							return string.Join(" * ", CS$<>8__locals2.arguments.Select((string x, int i) => base.<Translate>g__ArgMaybeWithParens|1(i)));
						}
						break;
					case 'n':
						if (functionName == "operators.__minus__")
						{
							return FormattableString.Invariant(FormattableStringFactory.Create("{0} - {1}", new object[]
							{
								CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
								CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
							}));
						}
						break;
					case 'o':
						break;
					case 'p':
						if (functionName == "operators.__tuple__")
						{
							return "(" + string.Join(", ", CS$<>8__locals2.arguments) + ")";
						}
						break;
					default:
						if (c == 't')
						{
							if (functionName == "operators.__notin__")
							{
								return FormattableString.Invariant(FormattableStringFactory.Create("{0} not in {1}", new object[]
								{
									CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
									CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
								}));
							}
						}
						break;
					}
					break;
				}
				case 20:
				{
					char c = functionName[12];
					if (c != 'a')
					{
						if (c == 'e')
						{
							if (functionName == "operators.__equals__")
							{
								if (CS$<>8__locals2.arguments[1] == text2)
								{
									return FormattableString.Invariant(FormattableStringFactory.Create("{0} is None", new object[] { CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0) }));
								}
								return FormattableString.Invariant(FormattableStringFactory.Create("{0} == {1}", new object[]
								{
									CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
									CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
								}));
							}
						}
					}
					else if (functionName == "operators.__assign__")
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0} = {1}", new object[]
						{
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
						}));
					}
					break;
				}
				case 21:
					if (functionName == "operators.__getitem__")
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0}[{1}]", new object[]
						{
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
						}));
					}
					break;
				case 22:
					if (functionName == "operators.__divideby__")
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0} / {1}", new object[]
						{
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
						}));
					}
					break;
				case 23:
				{
					char c = functionName[12];
					if (c != 'f')
					{
						if (c == 'm')
						{
							if (functionName == "operators.__make_list__")
							{
								string text3 = string.Join(", ", CS$<>8__locals2.arguments);
								return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { text3 }));
							}
						}
					}
					else if (functionName == "operators.__for_in_if__")
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0} for {1} in {2}", new object[]
						{
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(2)
						})) + ((CS$<>8__locals2.arguments.Count == 4) ? FormattableString.Invariant(FormattableStringFactory.Create(" if {0}", new object[] { CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(3) })) : string.Empty);
					}
					break;
				}
				case 24:
					if (functionName == "operators.__not_equals__")
					{
						if (CS$<>8__locals2.arguments[1] == text2)
						{
							return FormattableString.Invariant(FormattableStringFactory.Create("{0} is not None", new object[] { CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0) }));
						}
						return FormattableString.Invariant(FormattableStringFactory.Create("{0} != {1}", new object[]
						{
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
						}));
					}
					break;
				case 25:
					if (functionName == "operators.__intdivideby__")
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0} // {1}", new object[]
						{
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
						}));
					}
					break;
				case 28:
					if (functionName == "operators.__getitem_slice2__")
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0}[{1}:{2}]", new object[]
						{
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(2)
						}));
					}
					break;
				case 36:
					if (functionName == "operators.__getitem_slice_end_only__")
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0}[:{1}]", new object[]
						{
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
						}));
					}
					break;
				case 38:
					if (functionName == "operators.__getitem_slice_start_only__")
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0}[{1}:]", new object[]
						{
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(0),
							CS$<>8__locals2.<Translate>g__ArgMaybeWithParens|1(1)
						}));
					}
					break;
				}
			}
			if (CS$<>8__locals2.<asApp>5__2.FunctionName.Contains('.'))
			{
				throw new Exception("Unrecognized special function name: " + CS$<>8__locals2.<asApp>5__2.FunctionName);
			}
			string text4 = (CS$<>8__locals2.<asApp>5__2.IsFunctionLocal ? string.Empty : (headerModuleName + "."));
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}({2})", new object[]
			{
				text4,
				CS$<>8__locals2.<asApp>5__2.FunctionName,
				string.Join(", ", CS$<>8__locals2.arguments)
			}));
		}

		// Token: 0x04000836 RID: 2102
		public static Type ListType = typeof(List<>);

		// Token: 0x04000837 RID: 2103
		public static Type BoolType = typeof(bool);

		// Token: 0x04000838 RID: 2104
		public static Type NumType = typeof(decimal);

		// Token: 0x04000839 RID: 2105
		public static Type StrType = typeof(string);

		// Token: 0x0400083A RID: 2106
		public static Type DateType = typeof(DateTime);

		// Token: 0x0400083B RID: 2107
		public static Type TimeType = typeof(TimeSpan);

		// Token: 0x0400083C RID: 2108
		public static Type ObjType = typeof(object);

		// Token: 0x0400083D RID: 2109
		private static SSAValue Regex = new SSALiteral(PythonExpressionUtils.ObjType, "regex");

		// Token: 0x0400083E RID: 2110
		private static SSAValue Datetime = new SSALiteral(PythonExpressionUtils.ObjType, "datetime");

		// Token: 0x0400083F RID: 2111
		private static SSAValue Math = new SSALiteral(PythonExpressionUtils.ObjType, "math");

		// Token: 0x04000840 RID: 2112
		public static SSAValue Decimal = new SSALiteral(PythonExpressionUtils.ObjType, "decimal");

		// Token: 0x04000841 RID: 2113
		public static SSARValue None = PythonExpressionUtils.MkLiteral("None");

		// Token: 0x04000842 RID: 2114
		public static SSARValue Zero = PythonExpressionUtils.MkLiteral(0U);

		// Token: 0x04000843 RID: 2115
		public static SSARValue Minus1 = PythonExpressionUtils.MkLiteral(-1m);

		// Token: 0x04000844 RID: 2116
		public static SSARValue Start = PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "start", Array.Empty<SSAValue>());

		// Token: 0x04000845 RID: 2117
		public static SSARValue End = PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "end", Array.Empty<SSAValue>());
	}
}
