using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000030 RID: 48
	public static class ExtensionMethods
	{
		// Token: 0x06000186 RID: 390 RVA: 0x000069D1 File Offset: 0x00004BD1
		internal static XmlReader CreateValidatingXmlReader(this SqlXml fuzzyMatchingXml)
		{
			return XmlReader.Create(fuzzyMatchingXml.CreateReader(), FuzzyLookupXmlBuilder.s_xmlReaderSettings);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000069E3 File Offset: 0x00004BE3
		internal static XmlReader CreateValidatingXmlReader(this SqlString fuzzyMatchingXml)
		{
			return XmlReader.Create(new StringReader(fuzzyMatchingXml.Value), FuzzyLookupXmlBuilder.s_xmlReaderSettings);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000069FC File Offset: 0x00004BFC
		public static XmlNamespaceManager CreateFL3XmlNamespaceManager(this XmlDocument d)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(d.NameTable);
			if (d.NameTable.Get(FuzzyLookupConstants.Namespace) != null)
			{
				xmlNamespaceManager.AddNamespace("ns", FuzzyLookupConstants.Namespace);
			}
			else
			{
				xmlNamespaceManager.AddNamespace("ns", string.Empty);
			}
			return xmlNamespaceManager;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006A4C File Offset: 0x00004C4C
		public static void GetOrCreateTokenIds(this ITokenIdProvider tokenIdProvider, IEnumerable<StringExtent> tokens, int domainId, ICollection<int> tokenIds)
		{
			foreach (StringExtent stringExtent in tokens)
			{
				tokenIds.Add(tokenIdProvider.GetOrCreateTokenId(stringExtent, domainId));
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006A9C File Offset: 0x00004C9C
		[Obsolete]
		public static void Tokenize(this IRecordTokenizer tokenizer, TokenizerContext tokenizerContext, ITokenIdProvider tokenIdProvider, int domainId, IDataRecord record, ArraySegmentBuilder<int> tokenSequence)
		{
			tokenizerContext.Reset();
			foreach (StringExtent stringExtent in tokenizer.Tokenize(tokenizerContext, record))
			{
				tokenSequence.Add(tokenIdProvider.GetOrCreateTokenId(stringExtent, domainId));
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006AFC File Offset: 0x00004CFC
		public static IRowsetSink FindRowsetSink<T>(this T t, string rowsetSinkName) where T : IRowsetConsumer
		{
			return t.RowsetSinks.Find(rowsetSinkName);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006B14 File Offset: 0x00004D14
		public static IRowsetSink Find<T>(this T t, string rowsetSinkName) where T : IList<IRowsetSink>
		{
			IList<IRowsetSink> list = t;
			foreach (IRowsetSink rowsetSink in list)
			{
				if (string.Equals(rowsetSink.Name, rowsetSinkName))
				{
					return rowsetSink;
				}
			}
			if (string.Equals("default", rowsetSinkName, 3) && list.Count >= 1)
			{
				return list[0];
			}
			throw new ArgumentException(string.Format("A RowsetSink with name '{0}' was not found.", rowsetSinkName));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public static void AddRecords<T>(this T t, DataTable rowset, IDomainManager domainManager) where T : IRowsetConsumer
		{
			t.AddRecords("default", new CreateDataReaderDelegate(rowset.CreateDataReader), domainManager);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006BBF File Offset: 0x00004DBF
		public static void AddRecords<T>(this T t, DataTable rowset, IDomainManager domainManager, RecordBinding recordBinding) where T : IRowsetConsumer
		{
			t.AddRecords("default", new CreateDataReaderDelegate(rowset.CreateDataReader), domainManager, recordBinding);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006BDF File Offset: 0x00004DDF
		public static void AddRecords<T>(this T t, CreateDataReaderDelegate createDataReader, IDomainManager domainManager) where T : IRowsetConsumer
		{
			t.AddRecords("default", createDataReader, domainManager);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006BF3 File Offset: 0x00004DF3
		public static void AddRecords<T>(this T t, CreateDataReaderDelegate createDataReader, IDomainManager domainManager, RecordBinding recordBinding) where T : IRowsetConsumer
		{
			t.AddRecords("default", createDataReader, domainManager, recordBinding);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006C08 File Offset: 0x00004E08
		public static void AddRecords<T>(this T t, string rowsetSinkName, CreateDataReaderDelegate createDataReader, IDomainManager domainManager) where T : IRowsetConsumer
		{
			IRowsetSink rowsetSink = t.FindRowsetSink(rowsetSinkName);
			ITokenIdProvider tokenIdProvider = null;
			if (domainManager is DomainManager)
			{
				tokenIdProvider = (domainManager as DomainManager).TokenIdProvider;
			}
			RecordBinding recordBinding;
			using (IDataReader dataReader = createDataReader())
			{
				recordBinding = new RecordBinding(dataReader.GetSchemaTable());
			}
			rowsetSink.AddRecords(createDataReader, domainManager, tokenIdProvider, recordBinding);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006C74 File Offset: 0x00004E74
		public static void AddRecords<T>(this T t, string rowsetSinkName, CreateDataReaderDelegate createDataReader, IDomainManager domainManager, RecordBinding recordBinding) where T : IRowsetConsumer
		{
			IRowsetSink rowsetSink = t.FindRowsetSink(rowsetSinkName);
			ITokenIdProvider tokenIdProvider = null;
			if (domainManager is DomainManager)
			{
				tokenIdProvider = (domainManager as DomainManager).TokenIdProvider;
			}
			rowsetSink.AddRecords(createDataReader, domainManager, tokenIdProvider, recordBinding);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public static void AddRecords<T>(this T t, CreateDataReaderDelegate createDataReader, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding) where T : IRowsetSink
		{
			T t2 = t;
			RowsetManager rowsetManager = new RowsetManager();
			rowsetManager.Rowsets.Add(new CreateDataReaderRowset
			{
				Name = "default",
				CreateDataReaderDelegate = createDataReader
			});
			rowsetManager.Rowsets.Default = "0";
			if (recordBinding != null)
			{
				rowsetManager.RecordBindings.Add(recordBinding);
				rowsetManager.RecordBindings.Default = "0";
			}
			IRecordUpdate recordUpdate = (IRecordUpdate)t2;
			using (IDataReader dataReader = createDataReader())
			{
				IUpdateContext updateContext = recordUpdate.BeginUpdate(dataReader.GetSchemaTable());
				if (updateContext is IRecordUpdateContextInitialize)
				{
					(updateContext as IRecordUpdateContextInitialize).Initialize(rowsetManager, domainManager, tokenIdProvider, recordBinding);
				}
				while (dataReader.Read())
				{
					recordUpdate.AddRecord(updateContext, dataReader);
				}
				recordUpdate.EndUpdate(updateContext);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006D84 File Offset: 0x00004F84
		public static RecordBinding GetRecordBinding(this IRowsetManager rowsetManager, string rowsetName, ConnectionManager connectionManager)
		{
			RecordBinding recordBinding;
			if (!rowsetManager.TryGetRecordBinding(rowsetName, connectionManager, out recordBinding))
			{
				throw new ArgumentException(string.Format("Rowset named {0} not found.", rowsetName));
			}
			return recordBinding;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006DAF File Offset: 0x00004FAF
		public static bool IsNullOrEmpty(this SqlString value)
		{
			return value.IsNull || string.IsNullOrEmpty(value.Value.Trim());
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public static ArraySegmentBuilder<TransformationMatch> Clone(this ArraySegmentBuilder<TransformationMatch> tranMatches, ISegmentAllocator<TransformationMatch> tranMatchAllocator, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			if (tranMatches.Count > 0)
			{
				ArraySegmentBuilder<TransformationMatch> arraySegmentBuilder = new ArraySegmentBuilder<TransformationMatch>
				{
					Array = new TransformationMatch[tranMatches.Count],
					Count = tranMatches.Count
				};
				int num = 0;
				foreach (TransformationMatch transformationMatch in tranMatches)
				{
					arraySegmentBuilder.Array[num] = transformationMatch.Clone(intAllocator, byteAllocator);
					num++;
				}
				return arraySegmentBuilder;
			}
			return new ArraySegmentBuilder<TransformationMatch>();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006E60 File Offset: 0x00005060
		public static ArraySegmentBuilder<WeightedTransformationMatch> Clone(this ArraySegmentBuilder<WeightedTransformationMatch> tranMatches, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			if (tranMatches.Count > 0)
			{
				ArraySegmentBuilder<WeightedTransformationMatch> arraySegmentBuilder = new ArraySegmentBuilder<WeightedTransformationMatch>
				{
					Array = new WeightedTransformationMatch[tranMatches.Count],
					Count = tranMatches.Count
				};
				int num = 0;
				foreach (WeightedTransformationMatch weightedTransformationMatch in tranMatches)
				{
					arraySegmentBuilder.Array[num] = weightedTransformationMatch.Clone(intAllocator, byteAllocator);
					num++;
				}
				return arraySegmentBuilder;
			}
			return new ArraySegmentBuilder<WeightedTransformationMatch>();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006EF0 File Offset: 0x000050F0
		public static void Add(this ArraySegmentBuilder<WeightedTransformationMatch> tml, WeightedTransformationMatch item)
		{
			tml.Add(item);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006EFC File Offset: 0x000050FC
		public static void Add(this ArraySegmentBuilder<WeightedTransformationMatch> tml, ArraySegment<WeightedTransformationMatch> items)
		{
			foreach (WeightedTransformationMatch weightedTransformationMatch in items)
			{
				tml.Add(weightedTransformationMatch);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006F4C File Offset: 0x0000514C
		public static void Add(this ArraySegmentBuilder<WeightedTransformationMatch> tml, ArraySegment<TransformationMatch> items)
		{
			foreach (TransformationMatch transformationMatch in items)
			{
				tml.Add(new WeightedTransformationMatch
				{
					Position = transformationMatch.Position,
					Transformation = new WeightedTransformation
					{
						Transformation = transformationMatch.Transformation
					}
				});
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006FD0 File Offset: 0x000051D0
		public static void Add(this ArraySegmentBuilder<TransformationMatch> tml, TransformationMatch item)
		{
			tml.Add(item);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006FDC File Offset: 0x000051DC
		public static void Add(this ArraySegmentBuilder<TransformationMatch> tml, int position, Transformation item)
		{
			tml.Add(new TransformationMatch
			{
				Position = position,
				Transformation = item
			});
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007008 File Offset: 0x00005208
		public static void Add(this ArraySegmentBuilder<TransformationMatch> tml, ArraySegment<TransformationMatch> items)
		{
			foreach (TransformationMatch transformationMatch in items)
			{
				tml.Add(transformationMatch);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007058 File Offset: 0x00005258
		public static ArraySegment<TransformationMatch> ToTransformationMatchArraySegment(this ArraySegmentBuilder<WeightedTransformationMatch> tml, ISegmentAllocator<TransformationMatch> allocator)
		{
			ArraySegment<TransformationMatch> arraySegment = allocator.New(tml.Count);
			for (int i = 0; i < tml.Count; i++)
			{
				arraySegment.Array[arraySegment.Offset + i] = new TransformationMatch
				{
					Position = tml.Array[i].Position,
					Transformation = tml.Array[i].Transformation.Transformation
				};
			}
			return arraySegment;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000070D8 File Offset: 0x000052D8
		public static void Write(this ArraySegmentBuilder<WeightedTransformationMatch> tml, BinaryWriter w)
		{
			w.Write(tml.Count);
			for (int i = 0; i < tml.Count; i++)
			{
				tml[i].Write(w);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007114 File Offset: 0x00005314
		public static void Read(this ArraySegmentBuilder<WeightedTransformationMatch> tml, BinaryReader r, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			tml.Reset();
			int num = r.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				tml.Add(new WeightedTransformationMatch(r, intAllocator, byteAllocator));
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007148 File Offset: 0x00005348
		public static void Write(this ArraySegmentBuilder<TransformationMatch> tml, BinaryWriter w)
		{
			w.Write(tml.Count);
			for (int i = 0; i < tml.Count; i++)
			{
				tml[i].Write(w);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007184 File Offset: 0x00005384
		public static void Read(this ArraySegmentBuilder<TransformationMatch> tml, BinaryReader r, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			int num = r.ReadInt32();
			tml.Reset();
			for (int i = 0; i < num; i++)
			{
				tml.Add(new TransformationMatch(r, allocator, byteAllocator));
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000071B8 File Offset: 0x000053B8
		public static string ToVerboseString<T>(this ArraySegmentBuilder<T> tml, ITokenIdProvider tokenIdProvider) where T : ITransformationMatch
		{
			return tml.ToVerboseString(tokenIdProvider, false);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000071C2 File Offset: 0x000053C2
		public static string ToVerboseString<T>(this ArraySegmentBuilder<T> tml, ITokenIdProvider tokenIdProvider, bool verbose) where T : ITransformationMatch
		{
			return tml.ToVerboseString(tokenIdProvider, verbose);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000071CC File Offset: 0x000053CC
		public static string ToVerboseString<T>(IEnumerable<T> transformationMatchList, ITokenIdProvider tokenIdProvider, bool verbose) where T : ITransformationMatch
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (T t in transformationMatchList)
			{
				ITransformationMatch transformationMatch = t;
				if (num++ > 0)
				{
					stringBuilder.Append("\n");
				}
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					int num4 = num3;
					Transformation transformation = transformationMatch.Transformation;
					if (num4 >= transformation.From.Count)
					{
						break;
					}
					transformation = transformationMatch.Transformation;
					int num5 = transformation.From[num3];
					if (num2++ > 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(tokenIdProvider.SupportsGetToken ? tokenIdProvider.GetToken(num5).ToString() : num5.ToString());
					num3++;
				}
				stringBuilder.Append("=>");
				num2 = 0;
				int num6 = 0;
				for (;;)
				{
					int num7 = num6;
					Transformation transformation = transformationMatch.Transformation;
					if (num7 >= transformation.To.Count)
					{
						break;
					}
					transformation = transformationMatch.Transformation;
					int num8 = transformation.To[num6];
					if (num2++ > 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(tokenIdProvider.SupportsGetToken ? tokenIdProvider.GetToken(num8).ToString() : num8.ToString());
					num6++;
				}
				if (verbose)
				{
					stringBuilder.AppendFormat("\t({0})", transformationMatch.Transformation.GetType().Name);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007378 File Offset: 0x00005578
		public static bool Contains(this IList<WeightedTransformationMatch> tml, TransformationMatch tm)
		{
			return Enumerable.Any<WeightedTransformationMatch>(tml, (WeightedTransformationMatch wtm) => wtm.Position == tm.Position && wtm.Transformation.Equals(tm.Transformation));
		}
	}
}
