using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003D7 RID: 983
	public sealed class DelimitedTokenizeTransform : OneToOneTransformBase, ITokenizeTransform, IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x060014FD RID: 5373 RVA: 0x0007987C File Offset: 0x00077A7C
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("WRDTOKNS", 65537U, 65537U, 65537U, "TokenizeTextTransform", null);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000798A0 File Offset: 0x00077AA0
		public DelimitedTokenizeTransform(DelimitedTokenizeTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "DelimitedTokenize", Contracts.CheckRef<DelimitedTokenizeTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsTextItem))
		{
			this._exes = new DelimitedTokenizeTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				this._exes[i] = new DelimitedTokenizeTransform.ColInfoEx(args, i);
			}
			this._columnType = new VectorType(TextType.Instance, 0);
			base.Metadata.Seal();
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00079928 File Offset: 0x00077B28
		public DelimitedTokenizeTransform(DelimitedTokenizeTransform.TokenizeArguments args, IHostEnvironment env, IDataView input, OneToOneColumn[] columns)
			: base(env, "DelimitedTokenize", columns, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsTextItem))
		{
			Contracts.CheckValue<DelimitedTokenizeTransform.TokenizeArguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, Utils.Size<OneToOneColumn>(columns) > 0, "column");
			this._exes = new DelimitedTokenizeTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				this._exes[i] = new DelimitedTokenizeTransform.ColInfoEx(args);
			}
			this._columnType = new VectorType(TextType.Instance, 0);
			base.Metadata.Seal();
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x000799CC File Offset: 0x00077BCC
		private DelimitedTokenizeTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsTextItem))
		{
			this._exes = new DelimitedTokenizeTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i] = new DelimitedTokenizeTransform.ColInfoEx(ctx);
			}
			this._columnType = new VectorType(TextType.Instance, 0);
			base.Metadata.Seal();
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00079A60 File Offset: 0x00077C60
		public static DelimitedTokenizeTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("DelimitedTokenize");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(DelimitedTokenizeTransform.GetVersionInfo());
			return HostExtensions.Apply<DelimitedTokenizeTransform>(h, "Loading Model", (IChannel ch) => new DelimitedTokenizeTransform(ctx, h, input));
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00079AF8 File Offset: 0x00077CF8
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(DelimitedTokenizeTransform.GetVersionInfo());
			base.SaveBase(ctx);
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i].Save(ctx);
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00079B4F File Offset: 0x00077D4F
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._columnType;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00079B58 File Offset: 0x00077D58
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			OneToOneTransformBase.ColInfo colInfo = this.Infos[iinfo];
			if (!colInfo.TypeSrc.IsVector)
			{
				return this.MakeGetterOne(input, iinfo);
			}
			return this.MakeGetterVec(input, iinfo);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00079C40 File Offset: 0x00077E40
		private ValueGetter<VBuffer<DvText>> MakeGetterOne(IRow input, int iinfo)
		{
			ValueGetter<DvText> getSrc = base.GetSrcGetter<DvText>(input, iinfo);
			DvText src = default(DvText);
			List<DvText> terms = new List<DvText>();
			char[] separators = this._exes[iinfo].Separators;
			return delegate(ref VBuffer<DvText> dst)
			{
				getSrc.Invoke(ref src);
				terms.Clear();
				this.AddTerms(src, separators, terms);
				DvText[] array = dst.Values;
				if (terms.Count > 0)
				{
					if (Utils.Size<DvText>(array) < terms.Count)
					{
						array = new DvText[terms.Count];
					}
					terms.CopyTo(array);
				}
				dst = new VBuffer<DvText>(terms.Count, array, dst.Indices);
			};
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00079D78 File Offset: 0x00077F78
		private ValueGetter<VBuffer<DvText>> MakeGetterVec(IRow input, int iinfo)
		{
			int vectorSize = this.Infos[iinfo].TypeSrc.VectorSize;
			ValueGetter<VBuffer<DvText>> getSrc = base.GetSrcGetter<VBuffer<DvText>>(input, iinfo);
			VBuffer<DvText> src = default(VBuffer<DvText>);
			List<DvText> terms = new List<DvText>();
			char[] separators = this._exes[iinfo].Separators;
			return delegate(ref VBuffer<DvText> dst)
			{
				getSrc.Invoke(ref src);
				terms.Clear();
				for (int i = 0; i < src.Count; i++)
				{
					this.AddTerms(src.Values[i], separators, terms);
				}
				DvText[] array = dst.Values;
				if (terms.Count > 0)
				{
					if (Utils.Size<DvText>(array) < terms.Count)
					{
						array = new DvText[terms.Count];
					}
					terms.CopyTo(array);
				}
				dst = new VBuffer<DvText>(terms.Count, array, dst.Indices);
			};
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00079DEC File Offset: 0x00077FEC
		private void AddTerms(DvText txt, char[] separators, List<DvText> terms)
		{
			DvText dvText = txt;
			if (separators.Length > 1)
			{
				while (dvText.HasChars)
				{
					DvText dvText2;
					dvText.SplitOne(separators, ref dvText2, ref dvText);
					dvText2 = dvText2.Trim();
					if (dvText2.HasChars)
					{
						terms.Add(dvText2);
					}
				}
				return;
			}
			char c = separators[0];
			while (dvText.HasChars)
			{
				DvText dvText3;
				dvText.SplitOne(c, ref dvText3, ref dvText);
				dvText3 = dvText3.Trim();
				if (dvText3.HasChars)
				{
					terms.Add(dvText3);
				}
			}
		}

		// Token: 0x04000C97 RID: 3223
		internal const string Summary = "The input to this transform is text, and the output is a vector of text containing the words (tokens) in the original text. The separator is space, but can be specified as any other character (or multiple characters) if needed.";

		// Token: 0x04000C98 RID: 3224
		public const string LoaderSignature = "TokenizeTextTransform";

		// Token: 0x04000C99 RID: 3225
		private const string RegistrationName = "DelimitedTokenize";

		// Token: 0x04000C9A RID: 3226
		private readonly DelimitedTokenizeTransform.ColInfoEx[] _exes;

		// Token: 0x04000C9B RID: 3227
		private readonly ColumnType _columnType;

		// Token: 0x020003D8 RID: 984
		public class Column : OneToOneColumn
		{
			// Token: 0x06001508 RID: 5384 RVA: 0x00079E68 File Offset: 0x00078068
			public static DelimitedTokenizeTransform.Column Parse(string str)
			{
				DelimitedTokenizeTransform.Column column = new DelimitedTokenizeTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06001509 RID: 5385 RVA: 0x00079E87 File Offset: 0x00078087
			public bool TryUnparse(StringBuilder sb)
			{
				return string.IsNullOrEmpty(this.termSeparators) && this.TryUnparseCore(sb);
			}

			// Token: 0x04000C9C RID: 3228
			[Argument(0, HelpText = "Comma separated set of term separator(s). Commonly: 'space', 'comma', 'semicolon' or other single character.", ShortName = "sep")]
			public string termSeparators;
		}

		// Token: 0x020003D9 RID: 985
		public abstract class ArgumentsBase
		{
			// Token: 0x04000C9D RID: 3229
			[Argument(0, HelpText = "Comma separated set of term separator(s). Commonly: 'space', 'comma', 'semicolon' or other single character.", ShortName = "sep")]
			public string termSeparators = "space";
		}

		// Token: 0x020003DA RID: 986
		public sealed class Arguments : DelimitedTokenizeTransform.ArgumentsBase
		{
			// Token: 0x04000C9E RID: 3230
			[Argument(4, HelpText = "New column definition(s)", ShortName = "col", SortOrder = 1)]
			public DelimitedTokenizeTransform.Column[] column;
		}

		// Token: 0x020003DB RID: 987
		public sealed class TokenizeArguments : DelimitedTokenizeTransform.ArgumentsBase
		{
		}

		// Token: 0x020003DC RID: 988
		private sealed class ColInfoEx
		{
			// Token: 0x0600150E RID: 5390 RVA: 0x00079ECC File Offset: 0x000780CC
			public ColInfoEx(DelimitedTokenizeTransform.Arguments args, int iinfo)
			{
				this.Separators = PredictionUtil.SeparatorFromString(args.column[iinfo].termSeparators ?? args.termSeparators);
				Contracts.CheckUserArg(Utils.Size<char>(this.Separators) > 0, "sep");
			}

			// Token: 0x0600150F RID: 5391 RVA: 0x00079F19 File Offset: 0x00078119
			public ColInfoEx(DelimitedTokenizeTransform.ArgumentsBase args)
			{
				this.Separators = PredictionUtil.SeparatorFromString(args.termSeparators);
				Contracts.CheckUserArg(Utils.Size<char>(this.Separators) > 0, "sep");
			}

			// Token: 0x06001510 RID: 5392 RVA: 0x00079F4A File Offset: 0x0007814A
			public ColInfoEx(ModelLoadContext ctx)
			{
				this.Separators = Utils.ReadCharArray(ctx.Reader);
				Contracts.CheckDecode(Utils.Size<char>(this.Separators) > 0);
			}

			// Token: 0x06001511 RID: 5393 RVA: 0x00079F76 File Offset: 0x00078176
			public void Save(ModelSaveContext ctx)
			{
				Utils.WriteCharArray(ctx.Writer, this.Separators);
			}

			// Token: 0x04000C9F RID: 3231
			public readonly char[] Separators;
		}
	}
}
