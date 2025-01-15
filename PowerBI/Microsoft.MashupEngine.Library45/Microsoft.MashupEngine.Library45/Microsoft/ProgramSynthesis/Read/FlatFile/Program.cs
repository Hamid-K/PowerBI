using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x0200124E RID: 4686
	public abstract class Program : Program<string, ITable<string>>, IEquatable<Program>
	{
		// Token: 0x06008CE2 RID: 36066 RVA: 0x001D9689 File Offset: 0x001D7889
		internal Program(readFlatFile program, IReadOnlyList<string> columnNames, IReadOnlyList<string> rawColumnNames)
			: base(program.Node, 0.0, null)
		{
			this.TypedProgram = program;
			this.RawColumnNames = rawColumnNames;
			this.ColumnNames = columnNames;
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x06008CE3 RID: 36067 RVA: 0x001D96B7 File Offset: 0x001D78B7
		public readFlatFile TypedProgram { get; }

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x06008CE4 RID: 36068 RVA: 0x001D96BF File Offset: 0x001D78BF
		public IReadOnlyList<string> RawColumnNames { get; }

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06008CE5 RID: 36069 RVA: 0x001D96C7 File Offset: 0x001D78C7
		public IReadOnlyList<string> ColumnNames { get; }

		// Token: 0x06008CE6 RID: 36070 RVA: 0x001D96CF File Offset: 0x001D78CF
		public sealed override ITable<string> Run(string input)
		{
			return this.Run(input, true);
		}

		// Token: 0x06008CE7 RID: 36071 RVA: 0x001D96DC File Offset: 0x001D78DC
		public ITable<string> Run(string input, bool trim)
		{
			if (input == null)
			{
				return null;
			}
			ITable<string> table = this.RunInternal(input, trim);
			return new Table<string>(table.ColumnNames, table.Rows.ToList<IEnumerable<string>>(), null);
		}

		// Token: 0x06008CE8 RID: 36072 RVA: 0x001D970E File Offset: 0x001D790E
		public ITable<string> Run(TextReader input)
		{
			if (input == null)
			{
				return null;
			}
			return this.RunInternal(input, true);
		}

		// Token: 0x06008CE9 RID: 36073 RVA: 0x001D971D File Offset: 0x001D791D
		public ITable<string> Run(TextReader input, bool trim)
		{
			if (input == null)
			{
				return null;
			}
			return this.RunInternal(input, trim);
		}

		// Token: 0x06008CEA RID: 36074
		protected abstract ITable<string> RunInternal(string input, bool trim);

		// Token: 0x06008CEB RID: 36075
		protected abstract ITable<string> RunInternal(TextReader input, bool trim);

		// Token: 0x06008CEC RID: 36076 RVA: 0x00002188 File Offset: 0x00000388
		public virtual string GetMCode(string binaryContent, string encoding, Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape)
		{
			return null;
		}

		// Token: 0x06008CED RID: 36077 RVA: 0x001D972C File Offset: 0x001D792C
		public override string Serialize(ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML)
		{
			return this.Serialize(serializationFormat.AsASTSerializationSettings());
		}

		// Token: 0x06008CEE RID: 36078 RVA: 0x001D973C File Offset: 0x001D793C
		protected virtual XElement SerializeXML(ASTSerializationSettings serializationSettings)
		{
			string text = base.Serialize(serializationSettings);
			XNode xnode = (serializationSettings.HasXml ? XElement.Parse(text) : new XCData(text));
			XElement xelement = new XElement("Read.FlatFile", new XElement("Program", xnode));
			if (this.RawColumnNames != null && !serializationSettings.HasOmitLiterals)
			{
				xelement.Add(this.RawColumnNames.CollectionToXML("RawColumnNames", "Item", ObjectFormatting.Literal, null, Array.Empty<Func<string, XAttribute>>()));
			}
			return xelement;
		}

		// Token: 0x06008CEF RID: 36079 RVA: 0x001D97BD File Offset: 0x001D79BD
		public override string Serialize(ASTSerializationSettings serializationSettings)
		{
			return new XDocument(new object[] { this.SerializeXML(serializationSettings) }).ToString();
		}

		// Token: 0x06008CF0 RID: 36080 RVA: 0x001D97D9 File Offset: 0x001D79D9
		public virtual bool Equals(Program other)
		{
			return other != null && base.Equals(other) && ValueEquality.Comparer.Equals(this.RawColumnNames, other.RawColumnNames);
		}

		// Token: 0x06008CF1 RID: 36081 RVA: 0x001D9808 File Offset: 0x001D7A08
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.RawColumnNames != null)
			{
				num = HashHelpers.Combine(num, this.RawColumnNames.OrderDependentHashCode<string>());
			}
			return num;
		}

		// Token: 0x06008CF2 RID: 36082 RVA: 0x001D9838 File Offset: 0x001D7A38
		public void Switch(Action<CsvProgram> csv, Action<FwProgram> fw, Action<ExtractionTextProgram> etext)
		{
			CsvProgram csvProgram = this as CsvProgram;
			if (csvProgram != null)
			{
				csv(csvProgram);
				return;
			}
			FwProgram fwProgram = this as FwProgram;
			if (fwProgram != null)
			{
				fw(fwProgram);
				return;
			}
			ExtractionTextProgram extractionTextProgram = this as ExtractionTextProgram;
			if (extractionTextProgram != null)
			{
				etext(extractionTextProgram);
				return;
			}
			throw new Exception(string.Format("Invalid program: {0}", base.GetType()));
		}

		// Token: 0x06008CF3 RID: 36083 RVA: 0x001D9890 File Offset: 0x001D7A90
		public T Switch<T>(Func<CsvProgram, T> csvFunc, Func<FwProgram, T> fwFunc, Func<ExtractionTextProgram, T> etextFunc)
		{
			CsvProgram csvProgram = this as CsvProgram;
			T t;
			if (csvProgram == null)
			{
				FwProgram fwProgram = this as FwProgram;
				if (fwProgram == null)
				{
					ExtractionTextProgram extractionTextProgram = this as ExtractionTextProgram;
					if (extractionTextProgram == null)
					{
						throw new Exception(string.Format("Invalid program: {0}", base.GetType()));
					}
					t = etextFunc(extractionTextProgram);
				}
				else
				{
					t = fwFunc(fwProgram);
				}
			}
			else
			{
				t = csvFunc(csvProgram);
			}
			return t;
		}

		// Token: 0x06008CF4 RID: 36084 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public virtual bool IsPySparkSupported()
		{
			return false;
		}

		// Token: 0x040039B6 RID: 14774
		public const string DSLName = "Read.FlatFile";

		// Token: 0x040039B7 RID: 14775
		protected static readonly GrammarBuilders Builder = GrammarBuilders.Instance(Language.Grammar);
	}
}
