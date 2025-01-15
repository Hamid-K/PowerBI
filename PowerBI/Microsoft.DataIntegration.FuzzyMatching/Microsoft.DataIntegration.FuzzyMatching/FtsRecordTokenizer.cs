using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.Win32;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public sealed class FtsRecordTokenizer : RecordTokenizerBase, IRecordTokenizer
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0002A561 File Offset: 0x00028761
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0002A569 File Offset: 0x00028769
		public int MaxTokenLength { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0002A572 File Offset: 0x00028772
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x0002A57A File Offset: 0x0002877A
		public int LocaleId { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0002A583 File Offset: 0x00028783
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0002A58B File Offset: 0x0002878B
		public string WordBreakerClassId { get; set; }

		// Token: 0x0600090E RID: 2318 RVA: 0x0002A594 File Offset: 0x00028794
		public FtsRecordTokenizer()
		{
			this.MaxTokenLength = 256;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002A5A7 File Offset: 0x000287A7
		public FtsRecordTokenizer(int localeId)
		{
			this.MaxTokenLength = 256;
			this.LocaleId = localeId;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0002A5C4 File Offset: 0x000287C4
		private void PrepareSchema(DataTable schemaTable, DomainBinding domainBinding, out FtsTokenizerContext context)
		{
			context = new FtsTokenizerContext(1048576);
			List<Column> columns = domainBinding.Columns;
			context.ColumnIndexes = new int[columns.Count];
			context.IsString = new bool[columns.Count];
			for (int i = 0; i < columns.Count; i++)
			{
				if (columns[i].Ordinal >= 0)
				{
					context.ColumnIndexes[i] = columns[i].Ordinal;
					if (schemaTable != null)
					{
						if (SchemaUtils.FindColumnSchemaRow(schemaTable, columns[i].Ordinal) == null)
						{
							throw new Exception(string.Format("Column with ordinal '{0}' not present.", columns[i].Ordinal));
						}
						if (columns[i].Type != null)
						{
							context.IsString[i] = columns[i].Type == typeof(string);
						}
					}
				}
				else
				{
					if (string.IsNullOrEmpty(domainBinding.Columns[i].Name))
					{
						throw new Exception(string.Format("Neither the column Name or column Ordinal was specified for a column in the domain binding for domain {0}", domainBinding.DomainName));
					}
					DataRow dataRow = SchemaUtils.FindColumnSchemaRow(schemaTable, columns[i].Name, true);
					Type type = (Type)dataRow[SchemaTableColumn.DataType];
					context.IsString[i] = type == typeof(string);
					context.ColumnIndexes[i] = (int)dataRow[SchemaTableColumn.ColumnOrdinal];
				}
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002A740 File Offset: 0x00028940
		private void LoadRegistryInformation()
		{
			if (this.m_registryInfo == null)
			{
				Dictionary<int, FtsRecordTokenizer.LanguageRegistryInfo> dictionary = new Dictionary<int, FtsRecordTokenizer.LanguageRegistryInfo>();
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ContentIndex\\Language"))
				{
					foreach (string text in registryKey.GetSubKeyNames())
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(text))
						{
							FtsRecordTokenizer.LanguageRegistryInfo languageRegistryInfo = new FtsRecordTokenizer.LanguageRegistryInfo();
							languageRegistryInfo.LanguageName = text;
							languageRegistryInfo.LocaleId = (int)registryKey2.GetValue("Locale");
							languageRegistryInfo.StemmerClass = (string)registryKey2.GetValue("StemmerClass");
							languageRegistryInfo.WBreakerClass = (string)registryKey2.GetValue("WBreakerClass");
							dictionary.Add(languageRegistryInfo.LocaleId, languageRegistryInfo);
						}
					}
				}
				this.m_registryInfo = dictionary;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002A83C File Offset: 0x00028A3C
		public void Prepare(DataTable schemaTable, DomainBinding domainBinding, out TokenizerContext _context)
		{
			this.LoadRegistryInformation();
			FtsTokenizerContext ftsTokenizerContext;
			this.PrepareSchema(schemaTable, domainBinding, out ftsTokenizerContext);
			Type type = null;
			if (this.WordBreakerClassId != null)
			{
				type = Type.GetTypeFromCLSID(new Guid(this.WordBreakerClassId));
			}
			else
			{
				FtsRecordTokenizer.LanguageRegistryInfo languageRegistryInfo;
				if (this.m_registryInfo.TryGetValue(this.LocaleId, ref languageRegistryInfo))
				{
					type = Type.GetTypeFromCLSID(new Guid(languageRegistryInfo.WBreakerClass));
				}
				else if (this.m_registryInfo.TryGetValue(0, ref languageRegistryInfo))
				{
					type = Type.GetTypeFromCLSID(new Guid(languageRegistryInfo.WBreakerClass));
				}
				if (type == null)
				{
					throw new Exception(string.Format("Unable to load language information for LocaleId {0}.  Try explicitly setting the WordBreakerClassId property.", this.LocaleId));
				}
			}
			ftsTokenizerContext.WordBreaker = Activator.CreateInstance(type) as IWordBreaker;
			if (ftsTokenizerContext.WordBreaker == null)
			{
				throw new Exception(string.Format("Unable to create WordBreaker with Class ID {0}", type.GUID));
			}
			bool flag;
			ftsTokenizerContext.WordBreaker.Init(false, this.MaxTokenLength, out flag);
			_context = ftsTokenizerContext;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002A928 File Offset: 0x00028B28
		private static uint pfnFillTextBuffer(ref TEXT_SOURCE pTextSource)
		{
			return 2147751808U;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002A930 File Offset: 0x00028B30
		public IEnumerable<StringExtent> Tokenize(TokenizerContext tokenizerContext, IDataRecord record)
		{
			FtsTokenizerContext ftsTokenizerContext = (FtsTokenizerContext)tokenizerContext;
			ftsTokenizerContext.Reset();
			ftsTokenizerContext.WordSink.TokenList.Clear();
			for (int i = 0; i < ftsTokenizerContext.ColumnIndexes.Length; i++)
			{
				ArraySegment<char> arraySegment;
				TokenizerCommon.GetString(record, ftsTokenizerContext.ColumnIndexes[i], ftsTokenizerContext.IsString[i], ftsTokenizerContext.m_charAllocator, out arraySegment, ref this.m_providerConvertsStringToChar);
				if (arraySegment.Count > 0)
				{
					StringNormalization.NormalizeString(base.NormalizationOptions.FoldStringFlags, base.NormalizationOptions.MapStringFlags, arraySegment, ftsTokenizerContext.m_charAllocator, out arraySegment);
					for (int j = 0; j < arraySegment.Count; j++)
					{
						if (base.IsDelimiter.Invoke(arraySegment.Array[arraySegment.Offset + j]))
						{
							arraySegment.Array[arraySegment.Offset + j] = ' ';
						}
					}
					string text = arraySegment.ToStringEx();
					TEXT_SOURCE text_SOURCE = new TEXT_SOURCE
					{
						awcBuffer = text,
						iCur = 0,
						iEnd = text.Length,
						pfnFillTextBuffer = new delFillTextBuffer(FtsRecordTokenizer.pfnFillTextBuffer)
					};
					ftsTokenizerContext.WordSink.LastPos = -1;
					ftsTokenizerContext.WordSink.sourceString = arraySegment;
					ftsTokenizerContext.WordBreaker.BreakText(ref text_SOURCE, ftsTokenizerContext.WordSink, null);
				}
			}
			return ftsTokenizerContext.WordSink.TokenList;
		}

		// Token: 0x04000390 RID: 912
		private bool m_providerConvertsStringToChar;

		// Token: 0x04000391 RID: 913
		private Dictionary<int, FtsRecordTokenizer.LanguageRegistryInfo> m_registryInfo;

		// Token: 0x02000183 RID: 387
		[Serializable]
		private class LanguageRegistryInfo
		{
			// Token: 0x04000628 RID: 1576
			public int LocaleId;

			// Token: 0x04000629 RID: 1577
			public string LanguageName;

			// Token: 0x0400062A RID: 1578
			public string NoiseFile;

			// Token: 0x0400062B RID: 1579
			public string StemmerClass;

			// Token: 0x0400062C RID: 1580
			public string WBreakerClass;
		}
	}
}
