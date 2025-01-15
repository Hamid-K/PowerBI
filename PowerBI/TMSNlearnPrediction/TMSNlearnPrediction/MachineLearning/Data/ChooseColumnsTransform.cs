using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000269 RID: 617
	public sealed class ChooseColumnsTransform : RowToRowTransformBase
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x0004C21D File Offset: 0x0004A41D
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CHSCOLSF", 65537U, 65537U, 65537U, "ChooseColumnsTransform", "ChooseColumnsFunction");
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0004C242 File Offset: 0x0004A442
		public ChooseColumnsTransform(ChooseColumnsTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "ChooseColumns", input)
		{
			Contracts.CheckValue<ChooseColumnsTransform.Arguments>(this._host, args, "args");
			this._bindings = new ChooseColumnsTransform.Bindings(args, this._input.Schema);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0004C27C File Offset: 0x0004A47C
		private ChooseColumnsTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			this._bindings = new ChooseColumnsTransform.Bindings(ctx, this._input.Schema);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0004C2E4 File Offset: 0x0004A4E4
		public static ChooseColumnsTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("ChooseColumns");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(ChooseColumnsTransform.GetVersionInfo());
			return HostExtensions.Apply<ChooseColumnsTransform>(h, "Loading Model", (IChannel ch) => new ChooseColumnsTransform(ctx, h, input));
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0004C379 File Offset: 0x0004A579
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(ChooseColumnsTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			this._bindings.Save(ctx);
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0004C3B5 File Offset: 0x0004A5B5
		public override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0004C3C0 File Offset: 0x0004A5C0
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return null;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0004C3D8 File Offset: 0x0004A5D8
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor(dependencies, rand);
			return new ChooseColumnsTransform.RowCursor(this._host, this._bindings, rowCursor, active);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0004C420 File Offset: 0x0004A620
		public sealed override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor[] rowCursorSet = this._input.GetRowCursorSet(ref consolidator, dependencies, n, rand);
			IRowCursor[] array = new IRowCursor[rowCursorSet.Length];
			for (int i = 0; i < rowCursorSet.Length; i++)
			{
				array[i] = new ChooseColumnsTransform.RowCursor(this._host, this._bindings, rowCursorSet[i], active);
			}
			return array;
		}

		// Token: 0x040007BD RID: 1981
		public const string LoaderSignature = "ChooseColumnsTransform";

		// Token: 0x040007BE RID: 1982
		internal const string LoaderSignatureOld = "ChooseColumnsFunction";

		// Token: 0x040007BF RID: 1983
		private const string RegistrationName = "ChooseColumns";

		// Token: 0x040007C0 RID: 1984
		private readonly ChooseColumnsTransform.Bindings _bindings;

		// Token: 0x0200026A RID: 618
		public enum HiddenColumnOption : byte
		{
			// Token: 0x040007C2 RID: 1986
			Drop = 1,
			// Token: 0x040007C3 RID: 1987
			Keep,
			// Token: 0x040007C4 RID: 1988
			Rename
		}

		// Token: 0x0200026B RID: 619
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000DB5 RID: 3509 RVA: 0x0004C4A0 File Offset: 0x0004A6A0
			public static ChooseColumnsTransform.Column Parse(string str)
			{
				ChooseColumnsTransform.Column column = new ChooseColumnsTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000DB6 RID: 3510 RVA: 0x0004C4BF File Offset: 0x0004A6BF
			public bool TryUnparse(StringBuilder sb)
			{
				return this.hidden == null && this.TryUnparseCore(sb);
			}

			// Token: 0x040007C5 RID: 1989
			[Argument(4, HelpText = "What to do with hidden columns")]
			public ChooseColumnsTransform.HiddenColumnOption? hidden;
		}

		// Token: 0x0200026C RID: 620
		public sealed class Arguments
		{
			// Token: 0x040007C6 RID: 1990
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public ChooseColumnsTransform.Column[] column;

			// Token: 0x040007C7 RID: 1991
			[Argument(4, HelpText = "What to do with hidden columns")]
			public ChooseColumnsTransform.HiddenColumnOption hidden = ChooseColumnsTransform.HiddenColumnOption.Drop;
		}

		// Token: 0x0200026D RID: 621
		private sealed class Bindings : ISchema
		{
			// Token: 0x06000DB9 RID: 3513 RVA: 0x0004C4F0 File Offset: 0x0004A6F0
			public Bindings(ChooseColumnsTransform.Arguments args, ISchema schemaInput)
			{
				this.Input = schemaInput;
				Contracts.Check(Enum.IsDefined(typeof(ChooseColumnsTransform.HiddenColumnOption), args.hidden), "hidden");
				this.HidDefault = args.hidden;
				this.RawInfos = new ChooseColumnsTransform.Bindings.RawColInfo[Utils.Size<ChooseColumnsTransform.Column>(args.column)];
				if (this.RawInfos.Length > 0)
				{
					HashSet<string> hashSet = new HashSet<string>();
					for (int i = 0; i < this.RawInfos.Length; i++)
					{
						ChooseColumnsTransform.Column column = args.column[i];
						string text = column.name;
						string text2 = column.source;
						if (string.IsNullOrWhiteSpace(text2))
						{
							text2 = text;
						}
						else if (string.IsNullOrWhiteSpace(text))
						{
							text = text2;
						}
						Contracts.CheckUserArg(!string.IsNullOrWhiteSpace(text), "name");
						if (!hashSet.Add(text))
						{
							throw Contracts.ExceptUserArg("column", "New column '{0}' specified multiple times", new object[] { text });
						}
						ChooseColumnsTransform.HiddenColumnOption hiddenColumnOption = column.hidden ?? args.hidden;
						Contracts.CheckUserArg(Enum.IsDefined(typeof(ChooseColumnsTransform.HiddenColumnOption), hiddenColumnOption), "hidden");
						this.RawInfos[i] = new ChooseColumnsTransform.Bindings.RawColInfo(text, text2, hiddenColumnOption);
					}
				}
				this.BuildInfos(out this.Infos, out this.NameToInfoIndex, true);
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x0004C658 File Offset: 0x0004A858
			private void BuildInfos(out ChooseColumnsTransform.Bindings.ColInfo[] infosArray, out Dictionary<string, int> nameToCol, bool user)
			{
				ChooseColumnsTransform.Bindings.RawColInfo[] array = this.RawInfos;
				List<int> list = new List<int>();
				bool flag = false;
				Dictionary<string, List<int>> dictionary = null;
				if (array.Length == 0)
				{
					List<ChooseColumnsTransform.Bindings.RawColInfo> list2 = new List<ChooseColumnsTransform.Bindings.RawColInfo>();
					for (int i = 0; i < this.Input.ColumnCount; i++)
					{
						string columnName = this.Input.GetColumnName(i);
						int num;
						if (this.Input.TryGetColumnIndex(columnName, ref num))
						{
							if (num == i)
							{
								ChooseColumnsTransform.Bindings.RawColInfo rawColInfo = new ChooseColumnsTransform.Bindings.RawColInfo(columnName, columnName, this.HidDefault);
								list2.Add(rawColInfo);
								list.Add(i);
							}
							else if (this.HidDefault != ChooseColumnsTransform.HiddenColumnOption.Drop)
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, List<int>>();
								}
								List<int> list3;
								if (!dictionary.TryGetValue(columnName, out list3))
								{
									list3 = (dictionary[columnName] = new List<int>());
								}
								list3.Add(i);
							}
						}
					}
					if (dictionary != null && this.HidDefault == ChooseColumnsTransform.HiddenColumnOption.Rename)
					{
						flag = true;
					}
					array = list2.ToArray();
				}
				else
				{
					foreach (ChooseColumnsTransform.Bindings.RawColInfo rawColInfo2 in array)
					{
						int num2;
						if (!this.Input.TryGetColumnIndex(rawColInfo2.Source, ref num2))
						{
							throw user ? Contracts.ExceptUserArg("column", "source column '{0}' not found", new object[] { rawColInfo2.Source }) : Contracts.ExceptDecode("source column '{0}' not found", new object[] { rawColInfo2.Source });
						}
						list.Add(num2);
						if (rawColInfo2.Hid != ChooseColumnsTransform.HiddenColumnOption.Drop)
						{
							if (dictionary == null)
							{
								dictionary = new Dictionary<string, List<int>>();
							}
							dictionary[rawColInfo2.Source] = null;
							if (rawColInfo2.Hid == ChooseColumnsTransform.HiddenColumnOption.Rename)
							{
								flag = true;
							}
						}
					}
					if (dictionary != null)
					{
						for (int k = 0; k < this.Input.ColumnCount; k++)
						{
							string columnName2 = this.Input.GetColumnName(k);
							List<int> list4;
							int num3;
							if (dictionary.TryGetValue(columnName2, out list4) && this.Input.TryGetColumnIndex(columnName2, ref num3) && num3 != k)
							{
								if (list4 == null)
								{
									list4 = (dictionary[columnName2] = new List<int>());
								}
								list4.Add(k);
							}
						}
					}
				}
				HashSet<string> hashSet = null;
				if (flag)
				{
					hashSet = new HashSet<string>(array.Select((ChooseColumnsTransform.Bindings.RawColInfo r) => r.Name));
				}
				List<ChooseColumnsTransform.Bindings.ColInfo> list5 = new List<ChooseColumnsTransform.Bindings.ColInfo>();
				for (int l = 0; l < array.Length; l++)
				{
					ChooseColumnsTransform.Bindings.RawColInfo rawColInfo3 = array[l];
					List<int> list6;
					int num6;
					ColumnType columnType;
					ChooseColumnsTransform.Bindings.ColInfo colInfo;
					if (rawColInfo3.Hid != ChooseColumnsTransform.HiddenColumnOption.Drop && dictionary != null && dictionary.TryGetValue(rawColInfo3.Source, out list6) && list6 != null)
					{
						int count = list5.Count;
						int num4 = 0;
						int num5 = list6.Count;
						while (--num5 >= 0)
						{
							num6 = list6[num5];
							columnType = this.Input.GetColumnType(num6);
							string text = rawColInfo3.Name;
							if (rawColInfo3.Hid == ChooseColumnsTransform.HiddenColumnOption.Rename)
							{
								text = ChooseColumnsTransform.Bindings.GetUniqueName(hashSet, text, ref num4);
							}
							colInfo = new ChooseColumnsTransform.Bindings.ColInfo(text, num6, columnType);
							list5.Insert(count, colInfo);
						}
					}
					num6 = list[l];
					columnType = this.Input.GetColumnType(num6);
					colInfo = new ChooseColumnsTransform.Bindings.ColInfo(rawColInfo3.Name, num6, columnType);
					list5.Add(colInfo);
				}
				infosArray = list5.ToArray();
				nameToCol = new Dictionary<string, int>(this.Infos.Length);
				for (int m = 0; m < this.Infos.Length; m++)
				{
					nameToCol[this.Infos[m].Name] = m;
				}
			}

			// Token: 0x06000DBB RID: 3515 RVA: 0x0004C9CC File Offset: 0x0004ABCC
			private static string GetUniqueName(HashSet<string> names, string name, ref int inc)
			{
				string text;
				do
				{
					text = string.Format("{0}_{1:000}", name, ++inc);
				}
				while (!names.Add(text));
				return text;
			}

			// Token: 0x06000DBC RID: 3516 RVA: 0x0004CA00 File Offset: 0x0004AC00
			public Bindings(ModelLoadContext ctx, ISchema schemaInput)
			{
				this.Input = schemaInput;
				this.HidDefault = (ChooseColumnsTransform.HiddenColumnOption)ctx.Reader.ReadByte();
				Contracts.CheckDecode(Enum.IsDefined(typeof(ChooseColumnsTransform.HiddenColumnOption), this.HidDefault));
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num >= 0);
				this.RawInfos = new ChooseColumnsTransform.Bindings.RawColInfo[num];
				if (num > 0)
				{
					HashSet<string> hashSet = new HashSet<string>();
					for (int i = 0; i < num; i++)
					{
						string text = ctx.LoadNonEmptyString();
						if (!hashSet.Add(text))
						{
							throw Contracts.ExceptDecode("New column '{0}' specified multiple times", new object[] { text });
						}
						string text2 = ctx.LoadNonEmptyString();
						ChooseColumnsTransform.HiddenColumnOption hiddenColumnOption = (ChooseColumnsTransform.HiddenColumnOption)ctx.Reader.ReadByte();
						Contracts.CheckDecode(Enum.IsDefined(typeof(ChooseColumnsTransform.HiddenColumnOption), hiddenColumnOption));
						this.RawInfos[i] = new ChooseColumnsTransform.Bindings.RawColInfo(text, text2, hiddenColumnOption);
					}
				}
				this.BuildInfos(out this.Infos, out this.NameToInfoIndex, false);
			}

			// Token: 0x06000DBD RID: 3517 RVA: 0x0004CB04 File Offset: 0x0004AD04
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write((byte)this.HidDefault);
				ctx.Writer.Write(this.RawInfos.Length);
				for (int i = 0; i < this.RawInfos.Length; i++)
				{
					ChooseColumnsTransform.Bindings.RawColInfo rawColInfo = this.RawInfos[i];
					ctx.SaveNonEmptyString(rawColInfo.Name);
					ctx.SaveNonEmptyString(rawColInfo.Source);
					ctx.Writer.Write((byte)rawColInfo.Hid);
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0004CB7A File Offset: 0x0004AD7A
			public int ColumnCount
			{
				get
				{
					return this.Infos.Length;
				}
			}

			// Token: 0x06000DBF RID: 3519 RVA: 0x0004CB84 File Offset: 0x0004AD84
			public bool TryGetColumnIndex(string name, out int col)
			{
				if (name == null)
				{
					col = 0;
					return false;
				}
				return this.NameToInfoIndex.TryGetValue(name, out col);
			}

			// Token: 0x06000DC0 RID: 3520 RVA: 0x0004CB9B File Offset: 0x0004AD9B
			public string GetColumnName(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this.Infos[col].Name;
			}

			// Token: 0x06000DC1 RID: 3521 RVA: 0x0004CBC4 File Offset: 0x0004ADC4
			public ColumnType GetColumnType(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this.Infos[col].TypeSrc;
			}

			// Token: 0x06000DC2 RID: 3522 RVA: 0x0004CBED File Offset: 0x0004ADED
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this.Input.GetMetadataTypes(this.Infos[col].Source);
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x0004CC24 File Offset: 0x0004AE24
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this.Input.GetMetadataTypeOrNull(kind, this.Infos[col].Source);
			}

			// Token: 0x06000DC4 RID: 3524 RVA: 0x0004CC70 File Offset: 0x0004AE70
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				this.Input.GetMetadata<TValue>(kind, this.Infos[col].Source, ref value);
			}

			// Token: 0x06000DC5 RID: 3525 RVA: 0x0004CCBD File Offset: 0x0004AEBD
			internal bool[] GetActive(Func<int, bool> predicate)
			{
				return Utils.BuildArray<bool>(this.ColumnCount, predicate);
			}

			// Token: 0x06000DC6 RID: 3526 RVA: 0x0004CCF0 File Offset: 0x0004AEF0
			internal Func<int, bool> GetDependencies(Func<int, bool> predicate)
			{
				bool[] active = new bool[this.Input.ColumnCount];
				for (int i = 0; i < this.Infos.Length; i++)
				{
					if (predicate(i))
					{
						active[this.Infos[i].Source] = true;
					}
				}
				return (int col) => 0 <= col && col < active.Length && active[col];
			}

			// Token: 0x040007C8 RID: 1992
			public readonly ISchema Input;

			// Token: 0x040007C9 RID: 1993
			public readonly ChooseColumnsTransform.Bindings.RawColInfo[] RawInfos;

			// Token: 0x040007CA RID: 1994
			public readonly ChooseColumnsTransform.HiddenColumnOption HidDefault;

			// Token: 0x040007CB RID: 1995
			public readonly ChooseColumnsTransform.Bindings.ColInfo[] Infos;

			// Token: 0x040007CC RID: 1996
			public readonly Dictionary<string, int> NameToInfoIndex;

			// Token: 0x0200026E RID: 622
			public sealed class RawColInfo
			{
				// Token: 0x06000DC8 RID: 3528 RVA: 0x0004CD56 File Offset: 0x0004AF56
				public RawColInfo(string name, string source, ChooseColumnsTransform.HiddenColumnOption hid)
				{
					this.Name = name;
					this.Source = source;
					this.Hid = hid;
				}

				// Token: 0x040007CE RID: 1998
				public readonly string Name;

				// Token: 0x040007CF RID: 1999
				public readonly string Source;

				// Token: 0x040007D0 RID: 2000
				public readonly ChooseColumnsTransform.HiddenColumnOption Hid;
			}

			// Token: 0x0200026F RID: 623
			public sealed class ColInfo
			{
				// Token: 0x06000DC9 RID: 3529 RVA: 0x0004CD73 File Offset: 0x0004AF73
				public ColInfo(string name, int src, ColumnType typeSrc)
				{
					this.Name = name;
					this.Source = src;
					this.TypeSrc = typeSrc;
				}

				// Token: 0x040007D1 RID: 2001
				public readonly string Name;

				// Token: 0x040007D2 RID: 2002
				public readonly int Source;

				// Token: 0x040007D3 RID: 2003
				public readonly ColumnType TypeSrc;
			}
		}

		// Token: 0x02000270 RID: 624
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x06000DCA RID: 3530 RVA: 0x0004CD90 File Offset: 0x0004AF90
			public RowCursor(IChannelProvider provider, ChooseColumnsTransform.Bindings bindings, IRowCursor input, bool[] active)
				: base(provider, input)
			{
				this._bindings = bindings;
				this._active = active;
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0004CDA9 File Offset: 0x0004AFA9
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x06000DCC RID: 3532 RVA: 0x0004CDB1 File Offset: 0x0004AFB1
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active == null || this._active[col];
			}

			// Token: 0x06000DCD RID: 3533 RVA: 0x0004CDE8 File Offset: 0x0004AFE8
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				ChooseColumnsTransform.Bindings.ColInfo colInfo = this._bindings.Infos[col];
				return base.Input.GetGetter<TValue>(colInfo.Source);
			}

			// Token: 0x040007D4 RID: 2004
			private readonly ChooseColumnsTransform.Bindings _bindings;

			// Token: 0x040007D5 RID: 2005
			private readonly bool[] _active;
		}
	}
}
