using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Data.Expr.Internal;
using Microsoft.MachineLearning.Internal.Lexer;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000194 RID: 404
	public sealed class ExprTransform : RowToRowTransformBase
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x0002F5D2 File Offset: 0x0002D7D2
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("EXPRTRNF", 65537U, 65537U, 65537U, "ExprTransform", "DemoTransform");
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002F5F8 File Offset: 0x0002D7F8
		public ExprTransform(ExprTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "Expression", input)
		{
			Contracts.CheckValue<ExprTransform.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, Utils.Size<ExprTransform.Column>(args.column) > 0, "column");
			this._bindings = new ExprTransform.Bindings(args, this._input.Schema);
			this._exprs = new string[args.column.Length];
			for (int i = 0; i < this._exprs.Length; i++)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[i];
				if (colInfo.SrcIndices.Length > 5)
				{
					throw Contracts.ExceptUserArg(this._host, "source", "Too many source columns, max is {0}", new object[] { 5 });
				}
				string text = args.column[i].expression;
				if (string.IsNullOrWhiteSpace(text))
				{
					text = args.expression;
				}
				Contracts.CheckUserArg(this._host, !string.IsNullOrWhiteSpace(text), "expr", "Must specify an expression");
				this._exprs[i] = text;
			}
			this.CompileLambdas(out this._fns, out this._perms, true);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002F734 File Offset: 0x0002D934
		private void CompileLambdas(out Delegate[] fns, out int[][] perms, bool user)
		{
			fns = new Delegate[this._exprs.Length];
			perms = new int[this._exprs.Length][];
			for (int i = 0; i < fns.Length; i++)
			{
				string text = this._exprs[i];
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[i];
				ColumnType[] array = new ColumnType[colInfo.SrcTypes.Length];
				int num = -1;
				for (int j = 0; j < array.Length; j++)
				{
					ColumnType columnType = colInfo.SrcTypes[j];
					if (columnType.IsVector)
					{
						if (num >= 0)
						{
							throw user ? Contracts.ExceptUserArg(this._host, "src", "Can have at most one vector-valued source column") : Contracts.ExceptDecode(this._host, "Can have at most one vector-valued source column");
						}
						num = j;
						array[j] = columnType.ItemType;
					}
					else
					{
						array[j] = columnType;
					}
				}
				int[] array2 = (perms[i] = Utils.GetIdentityPermutation(colInfo.SrcIndices.Length));
				if (num > 0)
				{
					array2[0] = num;
					for (int k = 1; k <= num; k++)
					{
						array2[k] = k - 1;
					}
				}
				StringCharCursor stringCharCursor = new StringCharCursor(text);
				List<Error> list;
				List<int> list2;
				LambdaNode lambdaNode = LambdaParser.Parse(out list, out list2, stringCharCursor, array2, array);
				if (Utils.Size<Error>(list) > 0)
				{
					throw user ? Contracts.ExceptUserArg(this._host, "expr", "parsing failed: {0}", new object[] { list[0].GetMessage() }) : Contracts.ExceptDecode(this._host, "parsing failed: {0}", new object[] { list[0].GetMessage() });
				}
				using (IChannel ch = this._host.Start("LabmdaBinder.Run"))
				{
					LambdaBinder.Run(ref list, lambdaNode, delegate(string msg)
					{
						ch.Error(msg);
					});
					ch.Done();
				}
				if (Utils.Size<Error>(list) > 0)
				{
					throw user ? Contracts.ExceptUserArg(this._host, "expr", "binding failed: {0}", new object[] { list[0].GetMessage() }) : Contracts.ExceptDecode(this._host, "binding failed: {0}", new object[] { list[0].GetMessage() });
				}
				ColumnType columnType2 = lambdaNode.ResultType;
				if (num >= 0)
				{
					columnType2 = new VectorType(columnType2.AsPrimitive, colInfo.SrcTypes[num].AsVector);
				}
				this._bindings.Types[i] = columnType2;
				Delegate @delegate = LambdaCompiler.Compile(out list, null, lambdaNode);
				if (Utils.Size<Error>(list) > 0)
				{
					throw user ? Contracts.ExceptUserArg(this._host, "expr", "generating code failed: {0}", new object[] { list[0].GetMessage() }) : Contracts.ExceptDecode(this._host, "generating code failed: {0}", new object[] { list[0].GetMessage() });
				}
				fns[i] = @delegate;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002FA60 File Offset: 0x0002DC60
		private ExprTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			this._bindings = new ExprTransform.Bindings(ctx, this._input.Schema);
			this._exprs = new string[this._bindings.Infos.Length];
			for (int i = 0; i < this._bindings.Infos.Length; i++)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[i];
				if (colInfo.SrcIndices.Length > 5)
				{
					throw Contracts.ExceptDecode(this._host, "source", new object[] { "Too many source columns, max is {0}", 5 });
				}
				string text = (this._exprs[i] = ctx.LoadNonEmptyString());
				Contracts.CheckDecode(this._host, !string.IsNullOrWhiteSpace(text));
			}
			this.CompileLambdas(out this._fns, out this._perms, false);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002FB80 File Offset: 0x0002DD80
		public static ExprTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("Expression");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(ExprTransform.GetVersionInfo());
			return HostExtensions.Apply<ExprTransform>(h, "Loading Model", (IChannel ch) => new ExprTransform(ctx, h, input));
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002FC18 File Offset: 0x0002DE18
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(ExprTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			this._bindings.Save(ctx);
			for (int i = 0; i < this._exprs.Length; i++)
			{
				ctx.SaveNonEmptyString(this._exprs[i]);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0002FC80 File Offset: 0x0002DE80
		public override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002FC88 File Offset: 0x0002DE88
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			if (this._bindings.AnyNewColumnsActive(predicate))
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002FCB4 File Offset: 0x0002DEB4
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor(dependencies, rand);
			return new ExprTransform.RowCursor(this._host, this, rowCursor, active);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002FCF8 File Offset: 0x0002DEF8
		public sealed override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor[] array = this._input.GetRowCursorSet(ref consolidator, dependencies, n, rand);
			if (array.Length == 1 && n > 1 && this._bindings.AnyNewColumnsActive(predicate))
			{
				array = DataViewUtils.CreateSplitCursors(out consolidator, this._host, array[0], n);
			}
			IRowCursor[] array2 = new IRowCursor[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new ExprTransform.RowCursor(this._host, this, array[i], active);
			}
			return array2;
		}

		// Token: 0x0400046E RID: 1134
		internal const string Summary = "Executes a given lambda expression on input column values to produce an output column value. Accepts multiple input columns to produce an output column. The input column types currently supported are Float, 4 byte integer, and Boolean. At most one input column can be vector valued, in which case the output column is also vector valued and the lambda is executed on each slot of the input vector. Here are some examples assuming one floating point valued input (possibly vector valued):\r\n1) expr={x : x / 256} divides the input value by 256, useful for pixel data.\r\n2) expr={x : x ?? -1} replaces missing values with -1.\r\n3) expr={x : isna(x) ? 1.0 : 0.0} and expr={x : float(isna(x))} both produce missing value indicator values.\r\nThese examples assume two numeric inputs, with at most one vector valued:\r\n1) expr={(x, y) : log(x / y)} computes log odds.\r\n2) expr={(x, y) : (x - y)^2} computes the square of the difference.\r\n3) expr={(x, y) : abs(x - y)^0.5} and expr={x : sqrt(abs(x - y))} both compute the square root of the absolute value of the difference.\r\n4) expr={(x, y) : x ?? y} produces x if it is not an NA value and y otherwise.\r\n5) expr={(r, a) : r * cosd(a)} and expr={(r, a) : r * sind(a)} convert from polar coordinates, with the angle in degrees, to rectangular coordinates. This is useful, for example, to convert wind speed and compass direction to north-ward and east-ward wind velocity components.";

		// Token: 0x0400046F RID: 1135
		private const int MaxSrc = 5;

		// Token: 0x04000470 RID: 1136
		public const string LoaderSignature = "ExprTransform";

		// Token: 0x04000471 RID: 1137
		public const string LoaderSignatureOld = "DemoTransform";

		// Token: 0x04000472 RID: 1138
		private const string RegistrationName = "Expression";

		// Token: 0x04000473 RID: 1139
		private readonly ExprTransform.Bindings _bindings;

		// Token: 0x04000474 RID: 1140
		private readonly string[] _exprs;

		// Token: 0x04000475 RID: 1141
		private readonly int[][] _perms;

		// Token: 0x04000476 RID: 1142
		private readonly Delegate[] _fns;

		// Token: 0x02000196 RID: 406
		public sealed class Column : ManyToOneColumn
		{
			// Token: 0x060008B2 RID: 2226 RVA: 0x00030124 File Offset: 0x0002E324
			public static ExprTransform.Column Parse(string str)
			{
				ExprTransform.Column column = new ExprTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x060008B3 RID: 2227 RVA: 0x00030143 File Offset: 0x0002E343
			public bool TryUnparse(StringBuilder sb)
			{
				return string.IsNullOrWhiteSpace(this.expression) && this.TryUnparseCore(sb);
			}

			// Token: 0x0400047D RID: 1149
			[Argument(0, ShortName = "expr")]
			public string expression;
		}

		// Token: 0x02000197 RID: 407
		public sealed class Arguments
		{
			// Token: 0x0400047E RID: 1150
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public ExprTransform.Column[] column;

			// Token: 0x0400047F RID: 1151
			[Argument(0, ShortName = "expr", SortOrder = 2)]
			public string expression;
		}

		// Token: 0x0200019B RID: 411
		private sealed class Bindings : ManyToOneColumnBindingsBase
		{
			// Token: 0x060008C0 RID: 2240 RVA: 0x000307B4 File Offset: 0x0002E9B4
			public Bindings(ExprTransform.Arguments args, ISchema schemaInput)
				: base(args.column, schemaInput, new Func<ColumnType[], string>(ExprTransform.Bindings.TestTypes))
			{
				this.Types = new ColumnType[args.column.Length];
			}

			// Token: 0x060008C1 RID: 2241 RVA: 0x000307E2 File Offset: 0x0002E9E2
			public Bindings(ModelLoadContext ctx, ISchema schemaInput)
				: base(ctx, schemaInput, new Func<ColumnType[], string>(ExprTransform.Bindings.TestTypes))
			{
				this.Types = new ColumnType[this.Infos.Length];
			}

			// Token: 0x060008C2 RID: 2242 RVA: 0x0003080C File Offset: 0x0002EA0C
			private static string TestTypes(ColumnType[] types)
			{
				for (int i = 0; i < types.Length; i++)
				{
					ColumnType itemType = types[i].ItemType;
					if (!itemType.IsBool && !itemType.IsText && itemType != NumberType.I4 && itemType != NumberType.I8 && itemType != NumberType.R4 && itemType != NumberType.R8)
					{
						return "Expected one of Bool, Text, I4, I8, R4 or R8";
					}
				}
				return null;
			}

			// Token: 0x060008C3 RID: 2243 RVA: 0x00030867 File Offset: 0x0002EA67
			protected override ColumnType GetColumnTypeCore(int iinfo)
			{
				return this.Types[iinfo];
			}

			// Token: 0x04000489 RID: 1161
			public readonly ColumnType[] Types;
		}

		// Token: 0x0200019C RID: 412
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00030871 File Offset: 0x0002EA71
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x060008C5 RID: 2245 RVA: 0x0003087C File Offset: 0x0002EA7C
			public RowCursor(IChannelProvider provider, ExprTransform parent, IRowCursor input, bool[] active)
				: base(provider, input)
			{
				this._parent = parent;
				this._bindings = parent._bindings;
				this._active = active;
				this._getters = new Delegate[this._bindings.Infos.Length];
				for (int i = 0; i < this._bindings.Infos.Length; i++)
				{
					if (this.IsIndexActive(i))
					{
						int[] array = this._parent._perms[i];
						if (!this._bindings.Types[i].IsVector)
						{
							this._getters[i] = this.MakeGetter(i);
						}
						else
						{
							this._getters[i] = this.MakeGetterVec(i);
						}
					}
				}
			}

			// Token: 0x060008C6 RID: 2246 RVA: 0x00030928 File Offset: 0x0002EB28
			private Delegate MakeGetter(int iinfo)
			{
				Type[] genericArguments = this._parent._fns[iinfo].GetType().GetGenericArguments();
				Func<int, ValueGetter<int>> func;
				switch (genericArguments.Length - 1)
				{
				case 1:
					func = new Func<int, ValueGetter<int>>(this.GetGetter<int, int>);
					break;
				case 2:
					func = new Func<int, ValueGetter<int>>(this.GetGetter<int, int, int>);
					break;
				case 3:
					func = new Func<int, ValueGetter<int>>(this.GetGetter<int, int, int, int>);
					break;
				case 4:
					func = new Func<int, ValueGetter<int>>(this.GetGetter<int, int, int, int, int>);
					break;
				case 5:
					func = new Func<int, ValueGetter<int>>(this.GetGetter<int, int, int, int, int, int>);
					break;
				default:
					throw Contracts.ExceptNotSupp(this._ch);
				}
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(genericArguments);
				return (Delegate)methodInfo.Invoke(this, new object[] { iinfo });
			}

			// Token: 0x060008C7 RID: 2247 RVA: 0x00030A2C File Offset: 0x0002EC2C
			private ValueGetter<TDst> GetGetter<T0, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				Func<T0, TDst> fn = (Func<T0, TDst>)this._parent._fns[iinfo];
				ValueGetter<T0> getSrc0 = base.Input.GetGetter<T0>(colInfo.SrcIndices[0]);
				T0 src0 = default(T0);
				return delegate(ref TDst dst)
				{
					getSrc0.Invoke(ref src0);
					dst = fn(src0);
				};
			}

			// Token: 0x060008C8 RID: 2248 RVA: 0x00030AEC File Offset: 0x0002ECEC
			private ValueGetter<TDst> GetGetter<T0, T1, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				Func<T0, T1, TDst> fn = (Func<T0, T1, TDst>)this._parent._fns[iinfo];
				ValueGetter<T0> getSrc0 = base.Input.GetGetter<T0>(colInfo.SrcIndices[0]);
				ValueGetter<T1> getSrc1 = base.Input.GetGetter<T1>(colInfo.SrcIndices[1]);
				T0 src0 = default(T0);
				T1 src1 = default(T1);
				return delegate(ref TDst dst)
				{
					getSrc0.Invoke(ref src0);
					getSrc1.Invoke(ref src1);
					dst = fn(src0, src1);
				};
			}

			// Token: 0x060008C9 RID: 2249 RVA: 0x00030BE8 File Offset: 0x0002EDE8
			private ValueGetter<TDst> GetGetter<T0, T1, T2, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				Func<T0, T1, T2, TDst> fn = (Func<T0, T1, T2, TDst>)this._parent._fns[iinfo];
				ValueGetter<T0> getSrc0 = base.Input.GetGetter<T0>(colInfo.SrcIndices[0]);
				ValueGetter<T1> getSrc1 = base.Input.GetGetter<T1>(colInfo.SrcIndices[1]);
				ValueGetter<T2> getSrc2 = base.Input.GetGetter<T2>(colInfo.SrcIndices[2]);
				T0 src0 = default(T0);
				T1 src1 = default(T1);
				T2 src2 = default(T2);
				return delegate(ref TDst dst)
				{
					getSrc0.Invoke(ref src0);
					getSrc1.Invoke(ref src1);
					getSrc2.Invoke(ref src2);
					dst = fn(src0, src1, src2);
				};
			}

			// Token: 0x060008CA RID: 2250 RVA: 0x00030D20 File Offset: 0x0002EF20
			private ValueGetter<TDst> GetGetter<T0, T1, T2, T3, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				Func<T0, T1, T2, T3, TDst> fn = (Func<T0, T1, T2, T3, TDst>)this._parent._fns[iinfo];
				ValueGetter<T0> getSrc0 = base.Input.GetGetter<T0>(colInfo.SrcIndices[0]);
				ValueGetter<T1> getSrc1 = base.Input.GetGetter<T1>(colInfo.SrcIndices[1]);
				ValueGetter<T2> getSrc2 = base.Input.GetGetter<T2>(colInfo.SrcIndices[2]);
				ValueGetter<T3> getSrc3 = base.Input.GetGetter<T3>(colInfo.SrcIndices[3]);
				T0 src0 = default(T0);
				T1 src1 = default(T1);
				T2 src2 = default(T2);
				T3 src3 = default(T3);
				return delegate(ref TDst dst)
				{
					getSrc0.Invoke(ref src0);
					getSrc1.Invoke(ref src1);
					getSrc2.Invoke(ref src2);
					getSrc3.Invoke(ref src3);
					dst = fn(src0, src1, src2, src3);
				};
			}

			// Token: 0x060008CB RID: 2251 RVA: 0x00030E98 File Offset: 0x0002F098
			private ValueGetter<TDst> GetGetter<T0, T1, T2, T3, T4, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				Func<T0, T1, T2, T3, T4, TDst> fn = (Func<T0, T1, T2, T3, T4, TDst>)this._parent._fns[iinfo];
				ValueGetter<T0> getSrc0 = base.Input.GetGetter<T0>(colInfo.SrcIndices[0]);
				ValueGetter<T1> getSrc1 = base.Input.GetGetter<T1>(colInfo.SrcIndices[1]);
				ValueGetter<T2> getSrc2 = base.Input.GetGetter<T2>(colInfo.SrcIndices[2]);
				ValueGetter<T3> getSrc3 = base.Input.GetGetter<T3>(colInfo.SrcIndices[3]);
				ValueGetter<T4> getSrc4 = base.Input.GetGetter<T4>(colInfo.SrcIndices[4]);
				T0 src0 = default(T0);
				T1 src1 = default(T1);
				T2 src2 = default(T2);
				T3 src3 = default(T3);
				T4 src4 = default(T4);
				return delegate(ref TDst dst)
				{
					getSrc0.Invoke(ref src0);
					getSrc1.Invoke(ref src1);
					getSrc2.Invoke(ref src2);
					getSrc3.Invoke(ref src3);
					getSrc4.Invoke(ref src4);
					dst = fn(src0, src1, src2, src3, src4);
				};
			}

			// Token: 0x060008CC RID: 2252 RVA: 0x00030F98 File Offset: 0x0002F198
			private Delegate MakeGetterVec(int iinfo)
			{
				Type[] genericArguments = this._parent._fns[iinfo].GetType().GetGenericArguments();
				Func<int, ValueGetter<VBuffer<int>>> func;
				switch (genericArguments.Length - 1)
				{
				case 1:
					func = new Func<int, ValueGetter<VBuffer<int>>>(this.GetGetterVec<int, int>);
					break;
				case 2:
					func = new Func<int, ValueGetter<VBuffer<int>>>(this.GetGetterVec<int, int, int>);
					break;
				case 3:
					func = new Func<int, ValueGetter<VBuffer<int>>>(this.GetGetterVec<int, int, int, int>);
					break;
				case 4:
					func = new Func<int, ValueGetter<VBuffer<int>>>(this.GetGetterVec<int, int, int, int, int>);
					break;
				case 5:
					func = new Func<int, ValueGetter<VBuffer<int>>>(this.GetGetterVec<int, int, int, int, int, int>);
					break;
				default:
					throw Contracts.ExceptNotSupp(this._ch);
				}
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(genericArguments);
				return (Delegate)methodInfo.Invoke(this, new object[] { iinfo });
			}

			// Token: 0x060008CD RID: 2253 RVA: 0x00031204 File Offset: 0x0002F404
			private ValueGetter<VBuffer<TDst>> GetGetterVec<T0, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				Func<T0, TDst> fn = (Func<T0, TDst>)this._parent._fns[iinfo];
				ValueGetter<VBuffer<T0>> getSrc0 = base.Input.GetGetter<VBuffer<T0>>(colInfo.SrcIndices[0]);
				VBuffer<T0> src0 = default(VBuffer<T0>);
				TDst dstDef = fn(default(T0));
				RefPredicate<TDst> isDefaultPredicate = Conversions.Instance.GetIsDefaultPredicate<TDst>(this._bindings.Types[iinfo].ItemType);
				if (isDefaultPredicate.Invoke(ref dstDef))
				{
					return delegate(ref VBuffer<TDst> dst)
					{
						getSrc0.Invoke(ref src0);
						int count = src0.Count;
						TDst[] array = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, count);
						for (int i = 0; i < count; i++)
						{
							array[i] = fn(src0.Values[i]);
						}
						int[] array2 = ExprTransform.RowCursor.CopyIndices<T0>(ref src0, dst.Indices);
						dst = new VBuffer<TDst>(src0.Length, count, array, array2);
					};
				}
				return delegate(ref VBuffer<TDst> dst)
				{
					getSrc0.Invoke(ref src0);
					int length = src0.Length;
					TDst[] array3 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, length);
					if (src0.IsDense)
					{
						for (int j = 0; j < length; j++)
						{
							array3[j] = fn(src0.Values[j]);
						}
					}
					else
					{
						int count2 = src0.Count;
						int k = 0;
						int num = 0;
						while (k < length)
						{
							if (num < count2 && src0.Indices[num] == k)
							{
								array3[k] = fn(src0.Values[num]);
								num++;
							}
							else
							{
								array3[k] = dstDef;
							}
							k++;
						}
					}
					dst = new VBuffer<TDst>(length, array3, dst.Indices);
				};
			}

			// Token: 0x060008CE RID: 2254 RVA: 0x000314C4 File Offset: 0x0002F6C4
			private ValueGetter<VBuffer<TDst>> GetGetterVec<T0, T1, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				int[] array = this._parent._perms[iinfo];
				RefPredicate<TDst> isDef = Conversions.Instance.GetIsDefaultPredicate<TDst>(this._bindings.Types[iinfo].ItemType);
				Func<T0, T1, TDst> fn = (Func<T0, T1, TDst>)this._parent._fns[iinfo];
				ValueGetter<VBuffer<T0>> getSrc0 = base.Input.GetGetter<VBuffer<T0>>(colInfo.SrcIndices[array[0]]);
				ValueGetter<T1> getSrc1 = base.Input.GetGetter<T1>(colInfo.SrcIndices[array[1]]);
				VBuffer<T0> src0 = default(VBuffer<T0>);
				T1 src1 = default(T1);
				return delegate(ref VBuffer<TDst> dst)
				{
					getSrc0.Invoke(ref src0);
					getSrc1.Invoke(ref src1);
					if (src0.IsDense)
					{
						int length = src0.Length;
						TDst[] array2 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, length);
						for (int i = 0; i < length; i++)
						{
							array2[i] = fn(src0.Values[i], src1);
						}
						dst = new VBuffer<TDst>(length, array2, dst.Indices);
						return;
					}
					int length2 = src0.Length;
					int count = src0.Count;
					TDst tdst = fn(default(T0), src1);
					if (isDef.Invoke(ref tdst))
					{
						TDst[] array3 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, count);
						for (int j = 0; j < count; j++)
						{
							array3[j] = fn(src0.Values[j], src1);
						}
						int[] array4 = ExprTransform.RowCursor.CopyIndices<T0>(ref src0, dst.Indices);
						dst = new VBuffer<TDst>(length2, count, array3, array4);
						return;
					}
					TDst[] array5 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, length2);
					int k = 0;
					int num = 0;
					while (k < length2)
					{
						if (num < count && src0.Indices[num] == k)
						{
							array5[k] = fn(src0.Values[num], src1);
							num++;
						}
						else
						{
							array5[k] = tdst;
						}
						k++;
					}
					dst = new VBuffer<TDst>(length2, array5, dst.Indices);
				};
			}

			// Token: 0x060008CF RID: 2255 RVA: 0x0003179C File Offset: 0x0002F99C
			private ValueGetter<VBuffer<TDst>> GetGetterVec<T0, T1, T2, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				int[] array = this._parent._perms[iinfo];
				RefPredicate<TDst> isDef = Conversions.Instance.GetIsDefaultPredicate<TDst>(this._bindings.Types[iinfo].ItemType);
				Func<T0, T1, T2, TDst> fn = (Func<T0, T1, T2, TDst>)this._parent._fns[iinfo];
				ValueGetter<VBuffer<T0>> getSrc0 = base.Input.GetGetter<VBuffer<T0>>(colInfo.SrcIndices[array[0]]);
				ValueGetter<T1> getSrc1 = base.Input.GetGetter<T1>(colInfo.SrcIndices[array[1]]);
				ValueGetter<T2> getSrc2 = base.Input.GetGetter<T2>(colInfo.SrcIndices[array[2]]);
				VBuffer<T0> src0 = default(VBuffer<T0>);
				T1 src1 = default(T1);
				T2 src2 = default(T2);
				return delegate(ref VBuffer<TDst> dst)
				{
					getSrc0.Invoke(ref src0);
					getSrc1.Invoke(ref src1);
					getSrc2.Invoke(ref src2);
					if (src0.IsDense)
					{
						int length = src0.Length;
						TDst[] array2 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, length);
						for (int i = 0; i < length; i++)
						{
							array2[i] = fn(src0.Values[i], src1, src2);
						}
						dst = new VBuffer<TDst>(length, array2, dst.Indices);
						return;
					}
					int length2 = src0.Length;
					int count = src0.Count;
					TDst tdst = fn(default(T0), src1, src2);
					if (isDef.Invoke(ref tdst))
					{
						TDst[] array3 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, count);
						for (int j = 0; j < count; j++)
						{
							array3[j] = fn(src0.Values[j], src1, src2);
						}
						int[] array4 = ExprTransform.RowCursor.CopyIndices<T0>(ref src0, dst.Indices);
						dst = new VBuffer<TDst>(length2, count, array3, array4);
						return;
					}
					TDst[] array5 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, length2);
					int k = 0;
					int num = 0;
					while (k < length2)
					{
						if (num < count && src0.Indices[num] == k)
						{
							array5[k] = fn(src0.Values[num], src1, src2);
							num++;
						}
						else
						{
							array5[k] = tdst;
						}
						k++;
					}
					dst = new VBuffer<TDst>(length2, array5, dst.Indices);
				};
			}

			// Token: 0x060008D0 RID: 2256 RVA: 0x00031AC4 File Offset: 0x0002FCC4
			private ValueGetter<VBuffer<TDst>> GetGetterVec<T0, T1, T2, T3, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				int[] array = this._parent._perms[iinfo];
				RefPredicate<TDst> isDef = Conversions.Instance.GetIsDefaultPredicate<TDst>(this._bindings.Types[iinfo].ItemType);
				Func<T0, T1, T2, T3, TDst> fn = (Func<T0, T1, T2, T3, TDst>)this._parent._fns[iinfo];
				ValueGetter<VBuffer<T0>> getSrc0 = base.Input.GetGetter<VBuffer<T0>>(colInfo.SrcIndices[array[0]]);
				ValueGetter<T1> getSrc1 = base.Input.GetGetter<T1>(colInfo.SrcIndices[array[1]]);
				ValueGetter<T2> getSrc2 = base.Input.GetGetter<T2>(colInfo.SrcIndices[array[2]]);
				ValueGetter<T3> getSrc3 = base.Input.GetGetter<T3>(colInfo.SrcIndices[array[3]]);
				VBuffer<T0> src0 = default(VBuffer<T0>);
				T1 src1 = default(T1);
				T2 src2 = default(T2);
				T3 src3 = default(T3);
				return delegate(ref VBuffer<TDst> dst)
				{
					getSrc0.Invoke(ref src0);
					getSrc1.Invoke(ref src1);
					getSrc2.Invoke(ref src2);
					getSrc3.Invoke(ref src3);
					if (src0.IsDense)
					{
						int length = src0.Length;
						TDst[] array2 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, length);
						for (int i = 0; i < length; i++)
						{
							array2[i] = fn(src0.Values[i], src1, src2, src3);
						}
						dst = new VBuffer<TDst>(length, array2, dst.Indices);
						return;
					}
					int length2 = src0.Length;
					int count = src0.Count;
					TDst tdst = fn(default(T0), src1, src2, src3);
					if (isDef.Invoke(ref tdst))
					{
						TDst[] array3 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, count);
						for (int j = 0; j < count; j++)
						{
							array3[j] = fn(src0.Values[j], src1, src2, src3);
						}
						int[] array4 = ExprTransform.RowCursor.CopyIndices<T0>(ref src0, dst.Indices);
						dst = new VBuffer<TDst>(length2, count, array3, array4);
						return;
					}
					TDst[] array5 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, length2);
					int k = 0;
					int num = 0;
					while (k < length2)
					{
						if (num < count && src0.Indices[num] == k)
						{
							array5[k] = fn(src0.Values[num], src1, src2, src3);
							num++;
						}
						else
						{
							array5[k] = tdst;
						}
						k++;
					}
					dst = new VBuffer<TDst>(length2, array5, dst.Indices);
				};
			}

			// Token: 0x060008D1 RID: 2257 RVA: 0x00031E40 File Offset: 0x00030040
			private ValueGetter<VBuffer<TDst>> GetGetterVec<T0, T1, T2, T3, T4, TDst>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				int[] array = this._parent._perms[iinfo];
				RefPredicate<TDst> isDef = Conversions.Instance.GetIsDefaultPredicate<TDst>(this._bindings.Types[iinfo].ItemType);
				Func<T0, T1, T2, T3, T4, TDst> fn = (Func<T0, T1, T2, T3, T4, TDst>)this._parent._fns[iinfo];
				ValueGetter<VBuffer<T0>> getSrc0 = base.Input.GetGetter<VBuffer<T0>>(colInfo.SrcIndices[array[0]]);
				ValueGetter<T1> getSrc1 = base.Input.GetGetter<T1>(colInfo.SrcIndices[array[1]]);
				ValueGetter<T2> getSrc2 = base.Input.GetGetter<T2>(colInfo.SrcIndices[array[2]]);
				ValueGetter<T3> getSrc3 = base.Input.GetGetter<T3>(colInfo.SrcIndices[array[3]]);
				ValueGetter<T4> getSrc4 = base.Input.GetGetter<T4>(colInfo.SrcIndices[array[4]]);
				VBuffer<T0> src0 = default(VBuffer<T0>);
				T1 src1 = default(T1);
				T2 src2 = default(T2);
				T3 src3 = default(T3);
				T4 src4 = default(T4);
				return delegate(ref VBuffer<TDst> dst)
				{
					getSrc0.Invoke(ref src0);
					getSrc1.Invoke(ref src1);
					getSrc2.Invoke(ref src2);
					getSrc3.Invoke(ref src3);
					getSrc4.Invoke(ref src4);
					if (src0.IsDense)
					{
						int length = src0.Length;
						TDst[] array2 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, length);
						for (int i = 0; i < length; i++)
						{
							array2[i] = fn(src0.Values[i], src1, src2, src3, src4);
						}
						dst = new VBuffer<TDst>(length, array2, dst.Indices);
						return;
					}
					int length2 = src0.Length;
					int count = src0.Count;
					TDst tdst = fn(default(T0), src1, src2, src3, src4);
					if (isDef.Invoke(ref tdst))
					{
						TDst[] array3 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, count);
						for (int j = 0; j < count; j++)
						{
							array3[j] = fn(src0.Values[j], src1, src2, src3, src4);
						}
						int[] array4 = ExprTransform.RowCursor.CopyIndices<T0>(ref src0, dst.Indices);
						dst = new VBuffer<TDst>(length2, count, array3, array4);
						return;
					}
					TDst[] array5 = ExprTransform.RowCursor.Ensure<TDst>(dst.Values, length2);
					int k = 0;
					int num = 0;
					while (k < length2)
					{
						if (num < count && src0.Indices[num] == k)
						{
							array5[k] = fn(src0.Values[num], src1, src2, src3, src4);
							num++;
						}
						else
						{
							array5[k] = tdst;
						}
						k++;
					}
					dst = new VBuffer<TDst>(length2, array5, dst.Indices);
				};
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x00031F78 File Offset: 0x00030178
			private static T[] Ensure<T>(T[] a, int len)
			{
				if (Utils.Size<T>(a) < len)
				{
					a = new T[len];
				}
				return a;
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x00031F8C File Offset: 0x0003018C
			private static int[] CopyIndices<T>(ref VBuffer<T> src, int[] dst)
			{
				if (src.IsDense || src.Count == 0)
				{
					return dst;
				}
				if (Utils.Size<int>(dst) < src.Count)
				{
					dst = new int[src.Count];
				}
				Array.Copy(src.Indices, dst, src.Count);
				return dst;
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x00031FD9 File Offset: 0x000301D9
			private bool IsIndexActive(int iinfo)
			{
				return this._active == null || this._active[this._bindings.MapIinfoToCol(iinfo)];
			}

			// Token: 0x060008D5 RID: 2261 RVA: 0x00031FF8 File Offset: 0x000301F8
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active == null || this._active[col];
			}

			// Token: 0x060008D6 RID: 2262 RVA: 0x0003202C File Offset: 0x0003022C
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				bool flag;
				int num = this._bindings.MapColumnIndex(out flag, col);
				if (flag)
				{
					return base.Input.GetGetter<TValue>(num);
				}
				ValueGetter<TValue> valueGetter = this._getters[num] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x060008D7 RID: 2263 RVA: 0x000320A2 File Offset: 0x000302A2
			private ValueGetter<T> GetSrcGetter<T>(int iinfo, int isrc)
			{
				return base.Input.GetGetter<T>(this._bindings.Infos[iinfo].SrcIndices[isrc]);
			}

			// Token: 0x0400048A RID: 1162
			private readonly ExprTransform _parent;

			// Token: 0x0400048B RID: 1163
			private readonly ExprTransform.Bindings _bindings;

			// Token: 0x0400048C RID: 1164
			private readonly bool[] _active;

			// Token: 0x0400048D RID: 1165
			private readonly Delegate[] _getters;
		}
	}
}
