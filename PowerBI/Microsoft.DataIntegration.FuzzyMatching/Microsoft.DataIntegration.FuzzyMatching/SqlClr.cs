using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Reflection;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000CF RID: 207
	public class SqlClr
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x00022F74 File Offset: 0x00021174
		[SqlFunction(Name = "RecordContext_Stub", DataAccess = 0, SystemDataAccess = 0, IsDeterministic = true, IsPrecise = true)]
		[return: SqlFacet(IsNullable = true, MaxSize = 8000, IsFixedLength = false)]
		public static byte[] RecordContext_Stub([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 recordBindingHandle, [SqlFacet(IsNullable = true, MaxSize = 8000, IsFixedLength = false)] byte[] record)
		{
			if (SqlClr.RecordContext_Empty == null)
			{
				Record record2 = new Record();
				record2.Values = new object[] { "", "", "" };
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				record2.Write(binaryWriter);
				binaryWriter.Flush();
				memoryStream.Flush();
				SqlClr.RecordContext_Empty = memoryStream.ToArray();
			}
			return SqlClr.RecordContext_Empty;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00022FE0 File Offset: 0x000211E0
		private static RecordContextBuilder GetRecordContextBuilder(DomainManager domainManager, RecordBinding recordBinding, JoinSide joinSide)
		{
			List<TemporalHandle> list = null;
			if (SqlClr.s_recordContextBuilderListHandle != null)
			{
				list = SqlClr.s_recordContextBuilderListHandle.TryGetObject() as List<TemporalHandle>;
			}
			SqlClrObjectManager sqlClrObjectManager;
			if (list == null)
			{
				sqlClrObjectManager = SqlClr.ObjectManager;
				lock (sqlClrObjectManager)
				{
					if (SqlClr.s_recordContextBuilderListHandle != null)
					{
						list = SqlClr.s_recordContextBuilderListHandle.TryGetObject() as List<TemporalHandle>;
					}
					if (list == null)
					{
						list = new List<TemporalHandle>();
						SqlClr.s_recordContextBuilderListHandle = SqlClr.ObjectManager.GetObjectHandle(SqlClr.ObjectManager.CreateReference(list));
					}
				}
			}
			RecordContextBuilder recordContextBuilder;
			for (int i = 0; i < list.Count; i++)
			{
				recordContextBuilder = list[i].TryGetObject() as RecordContextBuilder;
				if (recordContextBuilder == null)
				{
					list.RemoveAt(i);
					i--;
				}
				else if (recordContextBuilder.DomainManager == domainManager && recordContextBuilder.RecordBinding == recordBinding && recordContextBuilder.JoinSide == joinSide)
				{
					return recordContextBuilder;
				}
			}
			recordContextBuilder = new RecordContextBuilder
			{
				DomainManager = domainManager,
				RecordBinding = recordBinding,
				JoinSide = joinSide
			};
			sqlClrObjectManager = SqlClr.ObjectManager;
			lock (sqlClrObjectManager)
			{
				list.Insert(0, SqlClr.ObjectManager.GetObjectHandle(SqlClr.ObjectManager.CreateReference(recordContextBuilder)));
			}
			return recordContextBuilder;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00023114 File Offset: 0x00021314
		[SqlFunction(Name = "RecordContext", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = true)]
		[return: SqlFacet(IsNullable = true, MaxSize = 8000, IsFixedLength = false)]
		public static byte[] RecordContext([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 recordBindingHandle, [SqlFacet(IsNullable = true, MaxSize = 8000, IsFixedLength = false)] byte[] record)
		{
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
			object @object = SqlClr.ObjectManager.GetObject(recordBindingHandle.Value);
			if (record == null)
			{
				return null;
			}
			RecordBinding recordBinding;
			JoinSide joinSide;
			if (@object is SqlClrFuzzyIndex)
			{
				SqlClrFuzzyIndex sqlClrFuzzyIndex = @object as SqlClrFuzzyIndex;
				recordBinding = sqlClrFuzzyIndex.RecordBinding;
				recordBinding.JoinSide = sqlClrFuzzyIndex.JoinSide;
				joinSide = sqlClrFuzzyIndex.JoinSide;
			}
			else
			{
				if (!(@object is RecordBinding))
				{
					throw new Exception(string.Format("Object of type {0} referred to by recordBindingHandle must be either a FuzzyIndex or RecordBinding object.", @object.GetType()));
				}
				recordBinding = @object as RecordBinding;
				if (recordBinding.JoinSide == JoinSide.Undefined)
				{
					throw new Exception("JoinSide was not defined in the RecordBinding.");
				}
				joinSide = recordBinding.JoinSide;
			}
			RecordContextBuilder recordContextBuilder = SqlClr.GetRecordContextBuilder(domainManager, recordBinding, joinSide);
			TemporalHandle instance = recordContextBuilder.ObjectPool.GetInstance(SqlClr.ObjectManager);
			RecordContextBuilderInstance recordContextBuilderInstance = (RecordContextBuilderInstance)instance.GetObject();
			recordContextBuilderInstance.Record.Read(new BinaryReader(new MemoryStream(record)), recordContextBuilderInstance.m_charSegmentAllocator);
			recordContextBuilderInstance.LookupUpdateContext.TokenizeAndRuleMatch(recordContextBuilderInstance.Record);
			BinaryWriter binaryWriter = recordContextBuilderInstance.BinaryWriter;
			MemoryStream memoryStream = binaryWriter.BaseStream as MemoryStream;
			recordContextBuilderInstance.LookupUpdateContext.RecordContext.Write(binaryWriter);
			binaryWriter.Flush();
			binaryWriter.BaseStream.Flush();
			binaryWriter.BaseStream.SetLength(binaryWriter.BaseStream.Position);
			binaryWriter.BaseStream.Seek(0L, 0);
			binaryWriter.Seek(0, 0);
			byte[] array = memoryStream.ToArray();
			recordContextBuilder.ObjectPool.ReturnInstance(instance);
			return array;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002329C File Offset: 0x0002149C
		[Obsolete("Use CreateObject instead.")]
		[SqlProcedure(Name = "CreateComparer")]
		public static void CreateComparer([SqlFacet(IsNullable = false)] SqlXml configurationXml, [SqlFacet(IsNullable = true)] SqlString leftRecordBindingXml, [SqlFacet(IsNullable = true)] SqlString rightRecordBindingXml, [SqlFacet(IsNullable = true)] SqlBoolean overwriteExisting)
		{
			SqlClr.ObjectManager.Collect();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(configurationXml.CreateValidatingXmlReader());
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			Comparer comparer = new Comparer();
			XmlNode xmlNode;
			if ((xmlNode = xmlDocument.SelectSingleNode("/ns:Comparer", xmlNamespaceManager)) != null || (xmlNode = xmlDocument.SelectSingleNode("/ns:Object", xmlNamespaceManager)) != null)
			{
				comparer.ReadXml(new XmlNodeReader(xmlNode));
				RecordBinding recordBinding = null;
				List<DomainName> list = new List<DomainName>();
				if (!leftRecordBindingXml.IsNull)
				{
					recordBinding = new RecordBinding();
					recordBinding.ReadXml(leftRecordBindingXml.CreateValidatingXmlReader());
					foreach (string text in recordBinding.GetBoundDomainNames())
					{
						list.Add(new DomainName
						{
							Name = text
						});
					}
					if (rightRecordBindingXml.IsNull)
					{
						rightRecordBindingXml = leftRecordBindingXml;
					}
					comparer.LeftRecordBinding = recordBinding;
				}
				if (!rightRecordBindingXml.IsNull)
				{
					RecordBinding recordBinding2 = new RecordBinding();
					recordBinding2.ReadXml(rightRecordBindingXml.CreateValidatingXmlReader());
					foreach (string text2 in recordBinding2.GetBoundDomainNames())
					{
						DomainName domainName = new DomainName
						{
							Name = text2
						};
						if (!list.Contains(domainName))
						{
							list.Add(domainName);
						}
					}
					comparer.RightRecordBinding = recordBinding;
				}
				if (comparer.Domains.Count == 0)
				{
					comparer.Domains = list;
				}
				SqlClrObjectManager objectManager = SqlClr.ObjectManager;
				lock (objectManager)
				{
					if (overwriteExisting.Value && SqlClr.ObjectManager.Contains(comparer.Name))
					{
						SqlClr.ObjectManager.Drop(comparer.Name);
					}
					int num = SqlClr.ObjectManager.CreateReference(comparer.Name, comparer);
					SqlClr.ObjectManager.Commit(num, configurationXml);
				}
				return;
			}
			throw new ArgumentException("Must define a Comparer element.");
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0002346C File Offset: 0x0002166C
		[SqlFunction(Name = "RecordSimilarity", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = false)]
		[return: SqlFacet(IsNullable = true, IsFixedLength = true)]
		public static SqlDouble RecordSimilarity([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 comparerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] leftRecord, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] rightRecord, [SqlFacet(IsNullable = true)] SqlDouble threshold)
		{
			SqlClr.s_globalStatistics.RecordSimilarityCount += 1L;
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
			Comparer comparer;
			TemporalHandle temporalHandle;
			ComparerInstance comparerInstance;
			ComparisonResult comparisonResult = SqlClr.RecordSimilarityInternal(SqlClr.ObjectManager.GetObjectHandle(comparerHandle.Value), SqlClr.ObjectManager.GetObjectHandle(domainManagerHandle.Value), leftRecord, rightRecord, threshold, out comparer, out temporalHandle, out comparerInstance);
			SqlDouble sqlDouble = SqlDouble.Null;
			if (comparisonResult.Similarity >= threshold.Value)
			{
				sqlDouble = comparisonResult.Similarity;
			}
			comparer.ObjectPool.ReturnInstance(temporalHandle);
			return sqlDouble;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00023508 File Offset: 0x00021708
		[SqlFunction(Name = "RecordSimilarityXml", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = false)]
		[return: SqlFacet(IsNullable = true, IsFixedLength = true)]
		public static SqlString RecordSimilarityXml([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 comparerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] leftRecord, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] rightRecord, [SqlFacet(IsNullable = true)] SqlDouble threshold)
		{
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
			Comparer comparer;
			TemporalHandle temporalHandle;
			ComparerInstance comparerInstance;
			ComparisonResult comparisonResult = SqlClr.RecordSimilarityInternal(SqlClr.ObjectManager.GetObjectHandle(comparerHandle.Value), SqlClr.ObjectManager.GetObjectHandle(domainManagerHandle.Value), leftRecord, rightRecord, threshold, out comparer, out temporalHandle, out comparerInstance);
			SqlString @null = SqlString.Null;
			if (comparisonResult.Similarity >= threshold.Value)
			{
				@null..ctor(comparisonResult.ToXml(domainManager, comparerInstance.TransientTokenIdProvider, true));
			}
			comparer.ObjectPool.ReturnInstance(temporalHandle);
			return @null;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002359C File Offset: 0x0002179C
		private static ComparisonResult RecordSimilarityInternal(TemporalHandle comparerHandle, TemporalHandle domainManagerHandle, byte[] leftRecord, byte[] rightRecord, SqlDouble _threshold, out Comparer comparer, out TemporalHandle comparerInstanceHandle, out ComparerInstance comparerInstance)
		{
			double num = 0.0;
			if (!_threshold.IsNull)
			{
				num = _threshold.Value;
			}
			comparer = (Comparer)comparerHandle.GetObject();
			DomainManager domainManager = (DomainManager)domainManagerHandle.GetObject();
			comparerInstanceHandle = comparer.ObjectPool.GetInstance(domainManagerHandle, SqlClr.ObjectManager);
			comparerInstance = (ComparerInstance)comparerInstanceHandle.GetObject();
			comparerInstance.Comparer.Threshold = num;
			if (leftRecord != null)
			{
				comparerInstance.leftRecord.Read(new BinaryReader(new MemoryStream(leftRecord)), comparerInstance.m_charAllocator);
			}
			if (rightRecord != null)
			{
				comparerInstance.rightRecord.Read(new BinaryReader(new MemoryStream(rightRecord)), comparerInstance.m_charAllocator);
			}
			ComparisonResult comparisonResult;
			comparerInstance.Comparer.Compare(comparerInstance.ComparerSession, comparerInstance.leftRecord, comparerInstance.rightRecord, out comparisonResult);
			return comparisonResult;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00023681 File Offset: 0x00021881
		[SqlFunction(Name = "RecordContextSimilarity_Stub", DataAccess = 0, SystemDataAccess = 0, IsDeterministic = true, IsPrecise = false)]
		[return: SqlFacet(IsNullable = true, IsFixedLength = true)]
		public static SqlDouble RecordContextSimilarity_Stub([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 comparerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] leftRecordContext, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] rightRecordContext, [SqlFacet(IsNullable = true)] SqlDouble threshold)
		{
			return 0.0;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00023694 File Offset: 0x00021894
		[SqlFunction(Name = "RecordContextSimilarity", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = false)]
		[return: SqlFacet(IsNullable = true, IsFixedLength = true)]
		public static SqlDouble RecordContextSimilarity([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 comparerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] leftRecordContext, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] rightRecordContext, [SqlFacet(IsNullable = true)] SqlDouble threshold)
		{
			SqlClr.s_globalStatistics.RecordContextSimilarityCount += 1L;
			if (threshold.IsNull)
			{
				threshold = 0.0;
			}
			TemporalHandle objectHandle = SqlClr.ObjectManager.GetObjectHandle(domainManagerHandle.Value);
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
			Comparer comparer = (Comparer)SqlClr.ObjectManager.GetObject(comparerHandle.Value);
			TemporalHandle instance = comparer.ObjectPool.GetInstance(objectHandle, SqlClr.ObjectManager);
			ComparerInstance comparerInstance = (ComparerInstance)instance.GetObject();
			comparerInstance.Comparer.Threshold = threshold.Value;
			SqlDouble sqlDouble = SqlDouble.Null;
			if (leftRecordContext != null)
			{
				comparerInstance.leftRecordContext.Read(new BinaryReader(new MemoryStream(leftRecordContext)), comparerInstance.intAllocator, comparerInstance.byteAllocator);
			}
			if (rightRecordContext != null)
			{
				comparerInstance.rightRecordContext.Read(new BinaryReader(new MemoryStream(rightRecordContext)), comparerInstance.intAllocator, comparerInstance.byteAllocator);
			}
			comparerInstance.Comparer.ResetLeftRecord(comparerInstance.ComparerSession, comparerInstance.leftRecordContext);
			comparerInstance.Comparer.ResetRightRecord(comparerInstance.ComparerSession, comparerInstance.rightRecordContext);
			ComparisonResult comparisonResult;
			if (comparerInstance.Comparer.Compare(comparerInstance.ComparerSession, out comparisonResult))
			{
				sqlDouble = comparisonResult.Similarity;
			}
			comparer.ObjectPool.ReturnInstance(instance);
			return sqlDouble;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000237EC File Offset: 0x000219EC
		[SqlFunction(Name = "RecordContextSimilarityXml", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = false)]
		[return: SqlFacet(IsNullable = true, IsFixedLength = true)]
		public static SqlString RecordContextSimilarityXml([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 comparerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] leftRecordContext, [SqlFacet(IsNullable = false, MaxSize = 8000, IsFixedLength = false)] byte[] rightRecordContext, [SqlFacet(IsNullable = true)] SqlDouble threshold)
		{
			SqlClr.s_globalStatistics.RecordContextSimilarityCount += 1L;
			if (threshold.IsNull)
			{
				threshold = 0.0;
			}
			TemporalHandle objectHandle = SqlClr.ObjectManager.GetObjectHandle(domainManagerHandle.Value);
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
			Comparer comparer = (Comparer)SqlClr.ObjectManager.GetObject(comparerHandle.Value);
			TemporalHandle instance = comparer.ObjectPool.GetInstance(objectHandle, SqlClr.ObjectManager);
			ComparerInstance comparerInstance = (ComparerInstance)instance.GetObject();
			comparerInstance.Comparer.Threshold = threshold.Value;
			SqlDouble @null = SqlDouble.Null;
			if (leftRecordContext != null)
			{
				comparerInstance.leftRecordContext.Read(new BinaryReader(new MemoryStream(leftRecordContext)), comparerInstance.intAllocator, comparerInstance.byteAllocator);
			}
			if (rightRecordContext != null)
			{
				comparerInstance.rightRecordContext.Read(new BinaryReader(new MemoryStream(rightRecordContext)), comparerInstance.intAllocator, comparerInstance.byteAllocator);
			}
			comparerInstance.Comparer.ResetLeftRecord(comparerInstance.ComparerSession, comparerInstance.leftRecordContext);
			comparerInstance.Comparer.ResetRightRecord(comparerInstance.ComparerSession, comparerInstance.rightRecordContext);
			ComparisonResult comparisonResult;
			if (comparerInstance.Comparer.Compare(comparerInstance.ComparerSession, out comparisonResult))
			{
				comparisonResult.Similarity;
			}
			comparer.ObjectPool.ReturnInstance(instance);
			SqlString null2 = SqlString.Null;
			if (comparisonResult.Similarity >= threshold.Value)
			{
				null2..ctor(comparisonResult.ToXml(domainManager, comparerInstance.TransientTokenIdProvider, true));
			}
			comparer.ObjectPool.ReturnInstance(instance);
			return null2;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0002397A File Offset: 0x00021B7A
		[SqlFunction(Name = "ColumnSimilarity_Stub", DataAccess = 0, SystemDataAccess = 0, IsDeterministic = true, IsPrecise = false)]
		[return: SqlFacet(IsNullable = true, IsFixedLength = true)]
		public static SqlDouble ColumnSimilarity_Stub([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 comparerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] char[] leftText, [SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] char[] rightText, [SqlFacet(IsNullable = true)] SqlDouble threshold)
		{
			SqlClr.s_globalStatistics.ColumnSimilarityCount += 1L;
			return 0.0;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000239A0 File Offset: 0x00021BA0
		[SqlFunction(Name = "ColumnSimilarity", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = false)]
		[return: SqlFacet(IsNullable = true, IsFixedLength = true)]
		public static SqlDouble ColumnSimilarity([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 comparerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] char[] leftText, [SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] char[] rightText, [SqlFacet(IsNullable = true)] SqlDouble threshold)
		{
			SqlClr.s_globalStatistics.ColumnSimilarityCount += 1L;
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
			Comparer comparer;
			TemporalHandle temporalHandle;
			ComparerInstance comparerInstance;
			ComparisonResult comparisonResult = SqlClr.ColumnSimilarityInternal(comparerHandle.Value, domainManagerHandle.Value, domainManager, leftText, rightText, threshold, out comparer, out temporalHandle, out comparerInstance);
			SqlDouble sqlDouble = SqlDouble.Null;
			if (comparisonResult.Similarity >= threshold.Value)
			{
				sqlDouble = comparisonResult.Similarity;
			}
			comparer.ObjectPool.ReturnInstance(temporalHandle);
			return sqlDouble;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00023A2C File Offset: 0x00021C2C
		[SqlFunction(Name = "ColumnSimilarityXml", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = false)]
		[return: SqlFacet(IsNullable = true, IsFixedLength = true)]
		public static SqlString ColumnSimilarityXml([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 comparerHandle, [SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 domainManagerHandle, [SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] char[] leftText, [SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] char[] rightText, [SqlFacet(IsNullable = true)] SqlDouble threshold)
		{
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
			Comparer comparer;
			TemporalHandle temporalHandle;
			ComparerInstance comparerInstance;
			ComparisonResult comparisonResult = SqlClr.ColumnSimilarityInternal(comparerHandle.Value, domainManagerHandle.Value, domainManager, leftText, rightText, threshold, out comparer, out temporalHandle, out comparerInstance);
			SqlString @null = SqlString.Null;
			if (comparisonResult.Similarity >= threshold.Value)
			{
				@null..ctor(comparisonResult.ToXml(domainManager, comparerInstance.TransientTokenIdProvider, true));
			}
			comparer.ObjectPool.ReturnInstance(temporalHandle);
			return @null;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00023AAC File Offset: 0x00021CAC
		private static ComparisonResult ColumnSimilarityInternal(int comparerHandle, int domainManagerHandle, DomainManager domainManager, char[] leftText, char[] rightText, SqlDouble _threshold, out Comparer comparer, out TemporalHandle comparerInstanceHandle, out ComparerInstance comparerInstance)
		{
			double num = 0.0;
			if (!_threshold.IsNull)
			{
				num = _threshold.Value;
			}
			comparer = (Comparer)SqlClr.ObjectManager.GetObject(comparerHandle);
			TemporalHandle objectHandle = SqlClr.ObjectManager.GetObjectHandle(domainManagerHandle);
			comparerInstanceHandle = comparer.ObjectPool.GetInstance(objectHandle, SqlClr.ObjectManager);
			comparerInstance = (ComparerInstance)comparerInstanceHandle.GetObject();
			comparerInstance.Comparer.Threshold = num;
			if (leftText != null)
			{
				comparerInstance.leftRecord.Values[0] = new ArraySegment<char>(leftText);
			}
			if (rightText != null)
			{
				comparerInstance.rightRecord.Values[0] = new ArraySegment<char>(rightText);
			}
			ComparisonResult comparisonResult;
			comparerInstance.Comparer.Compare(comparerInstance.ComparerSession, comparerInstance.leftRecord, comparerInstance.rightRecord, out comparisonResult);
			return comparisonResult;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00023B8C File Offset: 0x00021D8C
		private static void CreateRecordXml(XmlWriter writer, string elementName, IDataRecord record)
		{
			writer.WriteStartElement(elementName);
			for (int i = 0; i < record.FieldCount; i++)
			{
				writer.WriteElementString(record.GetName(i), record[i].ToString());
			}
			writer.WriteEndElement();
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00023BD0 File Offset: 0x00021DD0
		[SqlProcedure(Name = "CreateDomainManager")]
		public static void CreateDomainManager([SqlFacet(IsNullable = false)] SqlXml domainManagerXml, [SqlFacet(IsNullable = true)] SqlXml rowsetManagerXml, [SqlFacet(IsNullable = true)] SqlBoolean overwriteExisting)
		{
			SqlClr.ObjectManager.Collect();
			if (overwriteExisting.IsNull)
			{
				overwriteExisting = false;
			}
			DomainManager domainManager;
			if (rowsetManagerXml.IsNull)
			{
				FuzzyLookupXmlBuilder.CreateDomainManager(domainManagerXml.CreateValidatingXmlReader(), out domainManager);
			}
			else
			{
				RowsetManager rowsetManager = new RowsetManager(rowsetManagerXml.CreateValidatingXmlReader());
				FuzzyLookupXmlBuilder.CreateDomainManager(domainManagerXml.CreateValidatingXmlReader(), rowsetManager, out domainManager);
			}
			SqlClr.ObjectManager.Commit(domainManager.Name, domainManager, domainManagerXml, overwriteExisting.Value);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00023C44 File Offset: 0x00021E44
		[SqlProcedure(Name = "AddRecords")]
		public static void AddRecords([SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] SqlString objectName, [SqlFacet(IsNullable = true)] SqlXml rowsetXml, [SqlFacet(IsNullable = true)] SqlString sinkName, [SqlFacet(IsNullable = false)] SqlBoolean autoCommit)
		{
			object @object = SqlClr.ObjectManager.GetObject(objectName.Value);
			IRowsetDefinition rowsetDefinition = new RowsetManager(rowsetXml.CreateValidatingXmlReader()).Rowsets["default"];
			if (!(@object is SqlClrFuzzyIndex))
			{
				throw new Exception(string.Format("Unable to update object of type {0} as it does not implement IRowsetConsumer.", @object.GetType()));
			}
			SqlClrFuzzyIndex sqlClrFuzzyIndex = (SqlClrFuzzyIndex)SqlClr.ObjectManager.GetObject(objectName.Value);
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(sqlClrFuzzyIndex.DomainManagerName);
			if (string.IsNullOrEmpty(rowsetDefinition.RidColumnName))
			{
				throw new ArgumentException("Must define the ridColumnName attribute on the rowset.");
			}
			using (ConnectionManager connectionManager = new ConnectionManager())
			{
				int num = sqlClrFuzzyIndex.Append(connectionManager, rowsetDefinition);
				if (num >= 0)
				{
					SqlContext.Pipe.Send(string.Format("({0} row(s) affected)", num));
				}
			}
			if (autoCommit.IsTrue)
			{
				SqlClr.ObjectManager.Commit(SqlClr.ObjectManager.GetHandle(sqlClrFuzzyIndex.DomainManagerName));
				return;
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00023D54 File Offset: 0x00021F54
		[SqlProcedure(Name = "RemoveRecords")]
		public static void RemoveRecords([SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] SqlString objectName, [SqlFacet(IsNullable = true)] SqlXml rowsetXml, [SqlFacet(IsNullable = true)] SqlString sinkName, [SqlFacet(IsNullable = false)] SqlBoolean autoCommit)
		{
			SqlClrFuzzyIndex sqlClrFuzzyIndex = (SqlClrFuzzyIndex)SqlClr.ObjectManager.GetObject(objectName.Value);
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(sqlClrFuzzyIndex.DomainManagerName);
			IRowsetDefinition rowsetDefinition = new RowsetManager(rowsetXml.CreateValidatingXmlReader()).Rowsets["default"];
			if (string.IsNullOrEmpty(rowsetDefinition.RidColumnName))
			{
				throw new ArgumentException("Must define the ridColumnName attribute on the rowset.");
			}
			using (ConnectionManager connectionManager = new ConnectionManager())
			{
				int num = sqlClrFuzzyIndex.Remove(connectionManager, domainManager, domainManager.TokenIdProvider, rowsetDefinition);
				if (num >= 0)
				{
					SqlContext.Pipe.Send(string.Format("({0} row(s) affected)", num));
				}
			}
			if (autoCommit.IsTrue)
			{
				SqlClr.ObjectManager.Commit(SqlClr.ObjectManager.GetHandle(sqlClrFuzzyIndex.DomainManagerName));
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00023E38 File Offset: 0x00022038
		[SqlProcedure(Name = "CreateIndex")]
		public static void CreateIndex([SqlFacet(IsNullable = false)] SqlXml indexXml, [SqlFacet(IsNullable = true)] SqlXml rowsetXml, [SqlFacet(IsNullable = true)] SqlBoolean overwriteExisting)
		{
			SqlClr.ObjectManager.Collect();
			if (indexXml.IsNull)
			{
				throw new ArgumentException("Must specify indexXml.");
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(indexXml.CreateValidatingXmlReader());
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode;
			if ((xmlNode = xmlDocument.SelectSingleNode("/*/ns:FuzzyIndex", xmlNamespaceManager)) == null && (xmlNode = xmlDocument.SelectSingleNode("//ns:FuzzyIndex", xmlNamespaceManager)) == null)
			{
				throw new ArgumentException("Must specify a FuzzyIndex element in indexXml.");
			}
			SqlClrFuzzyIndex sqlClrFuzzyIndex = new SqlClrFuzzyIndex(new XmlNodeReader(xmlNode));
			sqlClrFuzzyIndex.SchemaName = SqlClr.GetSchemaName();
			if (sqlClrFuzzyIndex.JoinSide == JoinSide.Undefined)
			{
				sqlClrFuzzyIndex.JoinSide = JoinSide.Right;
			}
			if (string.IsNullOrEmpty(sqlClrFuzzyIndex.Name))
			{
				throw new ArgumentException("Must specify FuzzyIndex Name attribute.");
			}
			if (string.IsNullOrEmpty(sqlClrFuzzyIndex.DomainManagerName))
			{
				throw new ArgumentException("Must specify FuzzyIndex DomainManagerName attribute.");
			}
			if (sqlClrFuzzyIndex.Lookups.Count > 1)
			{
				throw new ArgumentException("Only one FuzzyIndex\\Lookups\\Lookup element may be defined.");
			}
			if (sqlClrFuzzyIndex.RecordBinding == null && rowsetXml.IsNull)
			{
				throw new ArgumentException("Must specify a RowsetBinding in either rowsetXml or FuzzyIndex\\RecordBinding element.");
			}
			IRowsetDefinition rowsetDefinition = null;
			DataTable dataTable = null;
			RowsetManager rowsetManager = null;
			if (!rowsetXml.IsNull)
			{
				rowsetManager = new RowsetManager(rowsetXml.CreateValidatingXmlReader());
				rowsetDefinition = rowsetManager.Rowsets["default"];
				if (rowsetDefinition != null)
				{
					if (string.IsNullOrEmpty(rowsetDefinition.RidColumnName))
					{
						throw new ArgumentException("Must define the ridColumnName attribute on the rowset.");
					}
					using (ConnectionManager connectionManager = new ConnectionManager())
					{
						dataTable = rowsetDefinition.GetSchemaTable(connectionManager);
					}
				}
			}
			using (ConnectionManager connectionManager2 = new ConnectionManager())
			{
				if (sqlClrFuzzyIndex.RidType == null)
				{
					if (rowsetDefinition == null)
					{
						throw new ArgumentException("Must specify FuzzyIndex RidType attribute if not specifying a rowset in rowsetXml.");
					}
					if (!string.IsNullOrEmpty(rowsetDefinition.RidColumnName) && dataTable != null)
					{
						DataRow dataRow;
						if (!SchemaUtils.TryGetRow(dataTable, rowsetDefinition.RidColumnName, out dataRow))
						{
							throw new Exception(string.Format("RidColumnName {0} not found.", rowsetDefinition.RidColumnName));
						}
						sqlClrFuzzyIndex.RidType = (Type)dataRow[SchemaTableColumn.DataType];
					}
					if (sqlClrFuzzyIndex.RidType == null)
					{
						throw new Exception("Unable to determine the RidType.  Specify the FuzzyIndex RidType attribute in indexXml.");
					}
				}
				DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(sqlClrFuzzyIndex.DomainManagerName);
				if (overwriteExisting.IsNull)
				{
					overwriteExisting = false;
				}
				if (overwriteExisting.IsTrue && (!SqlName.IsNullOrEmpty(sqlClrFuzzyIndex.RecordContextTableName) || !SqlName.IsNullOrEmpty(sqlClrFuzzyIndex.SignatureTableName)))
				{
					throw new ArgumentException("May not specify overwriteExisting and explicit FuzzyIndex RecordContextTableName or SignatureTableName attributes.");
				}
				if (overwriteExisting.IsFalse && SqlClr.ObjectManager.Contains(sqlClrFuzzyIndex.Name))
				{
					throw new Exception(string.Format("An object with the name {0} already exists.", sqlClrFuzzyIndex.Name));
				}
				if (sqlClrFuzzyIndex.RecordBinding == null && rowsetManager != null && rowsetManager.RecordBindings.Count > 0)
				{
					sqlClrFuzzyIndex.RecordBinding = rowsetManager.RecordBindings["default"];
				}
				else if (sqlClrFuzzyIndex.RecordBinding == null)
				{
					RecordBinding recordBinding = new RecordBinding(dataTable);
					foreach (object obj in recordBinding.Schema.Rows)
					{
						string text = (string)((DataRow)obj)[SchemaTableColumn.ColumnName];
						if (string.IsNullOrEmpty(rowsetDefinition.RidColumnName) || !string.Equals(text, rowsetDefinition.RidColumnName))
						{
							if (!domainManager.DomainExists(text))
							{
								throw new Exception(string.Format("Since no FuzzyIndex\\RecordBinding element was specified, a default binding of every column as its own domain was attempted.  No domain named {0} was found.", text));
							}
							recordBinding.Bind(text, new string[] { text });
						}
					}
					sqlClrFuzzyIndex.RecordBinding = recordBinding;
				}
				else if (sqlClrFuzzyIndex.RecordBinding.Schema == null)
				{
					sqlClrFuzzyIndex.RecordBinding.Schema = dataTable;
				}
				if (sqlClrFuzzyIndex.Lookups.Count == 0)
				{
					Lookup lookup = new Lookup();
					lookup.Domains.AddRange(sqlClrFuzzyIndex.RecordBinding.GetBoundDomainNames());
					sqlClrFuzzyIndex.Lookups.Add(lookup);
				}
				if (sqlClrFuzzyIndex.Lookups[0].SignatureGenerator == null)
				{
					sqlClrFuzzyIndex.Lookups[0].SignatureGenerator = new SignatureGenerator(XmlReader.Create(new StringReader("\r\n                        <SignatureGenerator typeName=\"Microsoft.DataIntegration.FuzzyMatching.LshSignatureGenerator\">\r\n                          <Properties>\r\n                            <Property name=\"NumHashtables\">6</Property>\r\n                            <Property name=\"DimensionsPerSignature\">4</Property>\r\n                          </Properties>\r\n                        </SignatureGenerator>")));
				}
				if (sqlClrFuzzyIndex.Lookups[0].Comparer == null)
				{
					sqlClrFuzzyIndex.Lookups[0].Comparer = new Comparer(XmlReader.Create(new StringReader("\r\n                        <Comparer typeName=\"Microsoft.DataIntegration.FuzzyMatching.Extensions.CustomComparer\">\r\n                          <Properties>\r\n                            <Property name=\"ContainmentBias\">0.9</Property>\r\n                          </Properties>\r\n                        </Comparer>")));
				}
				SqlConnection connection = connectionManager2.GetConnection(ConnectionManager.ContextConnectionName);
				if (SqlName.IsNullOrEmpty(sqlClrFuzzyIndex.RecordTableName))
				{
					sqlClrFuzzyIndex.RecordTableName = SqlUtils.CreateUniqueIdentifier(connection, new SqlName(SqlClr.GetSchemaName(), string.Format("{0}_RREF", sqlClrFuzzyIndex.Name)));
				}
				if (SqlName.IsNullOrEmpty(sqlClrFuzzyIndex.RecordContextTableName))
				{
					sqlClrFuzzyIndex.RecordContextTableName = SqlUtils.CreateUniqueIdentifier(connection, new SqlName(SqlClr.GetSchemaName(), string.Format("{0}_RCTXT", sqlClrFuzzyIndex.Name)));
				}
				if (SqlName.IsNullOrEmpty(sqlClrFuzzyIndex.SignatureTableName))
				{
					sqlClrFuzzyIndex.SignatureTableName = SqlUtils.CreateUniqueIdentifier(connection, new SqlName(SqlClr.GetSchemaName(), string.Format("{0}_RSIG", sqlClrFuzzyIndex.Name)));
				}
				int num = sqlClrFuzzyIndex.Create(connectionManager2, domainManager, rowsetDefinition, sqlClrFuzzyIndex.RidType);
				if (num >= 0)
				{
					SqlContext.Pipe.Send(string.Format("({0} row(s) affected)", num));
				}
			}
			try
			{
				StringWriter stringWriter = new StringWriter();
				XmlWriter xmlWriter = XmlWriter.Create(stringWriter);
				sqlClrFuzzyIndex.WriteXml(xmlWriter);
				xmlWriter.Flush();
				SqlXml sqlXml = new SqlXml(XmlReader.Create(new StringReader(stringWriter.ToString())));
				SqlClr.ObjectManager.Commit(sqlClrFuzzyIndex.Name, sqlClrFuzzyIndex, sqlXml, overwriteExisting.Value);
			}
			catch (Exception ex)
			{
				using (ConnectionManager connectionManager3 = new ConnectionManager())
				{
					sqlClrFuzzyIndex.TryDropTables(connectionManager3);
				}
				throw ex;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002443C File Offset: 0x0002263C
		[SqlProcedure(Name = "FuzzyJoin")]
		public static void FuzzyJoin([SqlFacet(IsNullable = false, MaxSize = 256, IsFixedLength = false)] SqlString indexName, [SqlFacet(IsNullable = false)] SqlXml leftRowsetXml, [SqlFacet(IsNullable = true)] SqlDouble threshold, [SqlFacet(IsNullable = true)] SqlInt32 maxComparisons, [SqlFacet(IsNullable = true, MaxSize = 256, IsFixedLength = false)] SqlString comparerName, [SqlFacet(IsNullable = true)] SqlBoolean filterIdentityMatches)
		{
			if (threshold.IsNull)
			{
				threshold = 0.0;
			}
			if (maxComparisons.IsNull || maxComparisons.Value <= 0)
			{
				maxComparisons = int.MaxValue;
			}
			if (filterIdentityMatches.IsNull)
			{
				filterIdentityMatches = false;
			}
			SqlClrFuzzyIndex sqlClrFuzzyIndex = (SqlClrFuzzyIndex)SqlClr.ObjectManager.GetObject(indexName.Value);
			IDomainManager domainManager = (IDomainManager)SqlClr.ObjectManager.GetObject(sqlClrFuzzyIndex.DomainManagerName);
			RowsetManager rowsetManager = new RowsetManager(leftRowsetXml.CreateValidatingXmlReader());
			if (rowsetManager.Rowsets.Count == 0)
			{
				throw new ArgumentException("Must define at least one SqlRowset or InlineRowset.");
			}
			IRowsetDefinition rowsetDefinition = rowsetManager.Rowsets["default"];
			using (ConnectionManager connectionManager = new ConnectionManager())
			{
				RecordBinding recordBinding;
				if (rowsetManager.RecordBindings.Count > 0)
				{
					recordBinding = rowsetManager.RecordBindings["default"];
				}
				else
				{
					DataTable schemaTable = rowsetDefinition.GetSchemaTable(connectionManager);
					if (sqlClrFuzzyIndex.RecordBinding.Schema.Rows.Count != schemaTable.Rows.Count)
					{
						throw new ArgumentException("The number of columns in the left Rowset does not match the right Rowset.  Must define a RecordBinding element in rowsetXml.");
					}
					recordBinding = new RecordBinding(schemaTable);
					foreach (DomainBinding domainBinding in sqlClrFuzzyIndex.RecordBinding)
					{
						DomainBinding domainBinding2 = new DomainBinding();
						domainBinding2.DomainName = domainBinding.DomainName;
						foreach (Column column in domainBinding.Columns)
						{
							DataRow dataRow;
							if (column.Ordinal >= 0)
							{
								if (!SchemaUtils.TryGetRow(schemaTable, column.Ordinal, out dataRow))
								{
									throw new Exception(string.Format("A column with ordinal {0} corresponding to right column named {1} could not be found in the left rowset.  You may need to define an explicit RecordBinding element in rowsetXml.", column.Ordinal, column.Name));
								}
							}
							else if (!SchemaUtils.TryGetRow(schemaTable, column.Name, out dataRow))
							{
								int ordinal = SchemaUtils.GetOrdinal(sqlClrFuzzyIndex.RecordBinding.Schema, column.Name, false);
								if (!SchemaUtils.TryGetRow(schemaTable, ordinal, out dataRow))
								{
									throw new Exception(string.Format("A column with ordinal {0} corresponding to right column named {1} could not be found in the left rowset.  You may need to define an explicit RecordBinding element in rowsetXml.", ordinal, column.Name));
								}
							}
							domainBinding2.Columns.Add(new Column
							{
								Name = (string)dataRow[SchemaTableColumn.ColumnName],
								Ordinal = Convert.ToInt32(dataRow[SchemaTableColumn.ColumnOrdinal]),
								Type = (Type)dataRow[SchemaTableColumn.DataType]
							});
						}
						recordBinding.Add(domainBinding2);
					}
				}
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(leftRowsetXml.CreateValidatingXmlReader());
				XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/*/ns:Comparer", xmlNamespaceManager);
				if (xmlNode == null)
				{
					xmlNode = xmlDocument.SelectSingleNode("//ns:Comparer", xmlNamespaceManager);
				}
				Comparer comparer;
				if (xmlNode != null)
				{
					comparer = new Comparer(new XmlNodeReader(xmlNode));
				}
				else
				{
					if (sqlClrFuzzyIndex.Lookups[0].Comparer.TypeName.Length == 0)
					{
						throw new ArgumentException("There was no Comparer defined.  You must specify a Comparer XML element either in the XML passed to Join or in the Lookup definition for the right index.");
					}
					comparer = sqlClrFuzzyIndex.Lookups[0].Comparer;
				}
				SqlClrFuzzyIndex sqlClrFuzzyIndex2 = new SqlClrFuzzyIndex
				{
					Name = indexName.Value,
					SchemaName = SqlClr.GetSchemaName(),
					DomainManagerName = sqlClrFuzzyIndex.DomainManagerName,
					RecordTableName = new SqlName(string.Format("#{0}_LREF", indexName.Value)),
					RecordContextTableName = new SqlName(string.Format("#{0}_LCTXT", indexName.Value)),
					SignatureTableName = new SqlName(string.Format("#{0}_LSIG", indexName.Value)),
					Lookups = sqlClrFuzzyIndex.Lookups,
					JoinSide = JoinSide.Left,
					RecordBinding = recordBinding
				};
				try
				{
					sqlClrFuzzyIndex2.Create(connectionManager, domainManager, rowsetDefinition, null);
					string text = null;
					bool commitTemporaryObjectsToTempDb = SqlClr.ObjectManager.CommitTemporaryObjectsToTempDb;
					try
					{
						if (comparerName.IsNull)
						{
							Comparer comparer2 = new Comparer();
							comparer2.AssemblyName = comparer.AssemblyName;
							comparer2.TypeName = comparer.TypeName;
							comparer2.Properties = comparer.Properties;
							comparer2.Domains = sqlClrFuzzyIndex.Lookups[0].Domains.ConvertAll<DomainName>((string n) => new DomainName
							{
								Name = n
							});
							comparer2.ExactMatchDomains = sqlClrFuzzyIndex.Lookups[0].ExactMatchDomains.ConvertAll<DomainName>((string n) => new DomainName
							{
								Name = n
							});
							comparer2.LeftRecordBinding = recordBinding;
							comparer2.RightRecordBinding = sqlClrFuzzyIndex.RecordBinding;
							Comparer comparer3 = comparer2;
							text = Guid.NewGuid().ToString();
							int num = SqlClr.ObjectManager.CreateReference(text, comparer3);
							if (commitTemporaryObjectsToTempDb)
							{
								SqlClr.ObjectManager.Commit(num, null, connectionManager, ConnectionManager.ContextConnectionName, true);
							}
						}
						else
						{
							text = comparerName.Value;
						}
						using (SqlCommand sqlCommand = connectionManager.GetConnection(ConnectionManager.ContextConnectionName).CreateCommand())
						{
							sqlCommand.CommandTimeout = 0;
							sqlCommand.CommandText = SqlClrFuzzyIndex.CreateJoinQuery(text, sqlClrFuzzyIndex2, sqlClrFuzzyIndex, SqlClr.GetSchemaName(), threshold.Value, maxComparisons.Value, filterIdentityMatches.Value, false);
							SqlContext.Pipe.ExecuteAndSend(sqlCommand);
						}
					}
					finally
					{
						if (comparerName.IsNull)
						{
							SqlClr.ObjectManager.Drop(text, connectionManager, false, commitTemporaryObjectsToTempDb);
						}
					}
				}
				finally
				{
					sqlClrFuzzyIndex2.TryDropTables(connectionManager);
				}
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00024A58 File Offset: 0x00022C58
		[SqlFunction(Name = "Signatures_Stub", FillRowMethodName = "FillSignaturesRow", TableDefinition = "Signature bigint", DataAccess = 0, SystemDataAccess = 0, IsDeterministic = true, IsPrecise = true)]
		public static IEnumerable Signatures_Stub([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 signatureGeneratorHandle, [SqlFacet(IsNullable = true, IsFixedLength = false)] byte[] recordContext)
		{
			return null;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00024A5C File Offset: 0x00022C5C
		[SqlFunction(Name = "Signatures", FillRowMethodName = "FillSignaturesRow", TableDefinition = "Signature bigint", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = true)]
		public static IEnumerable Signatures([SqlFacet(IsNullable = false, IsFixedLength = true)] SqlInt32 signatureGeneratorHandle, [SqlFacet(IsNullable = true, IsFixedLength = false)] byte[] recordContext)
		{
			SqlClr.s_globalStatistics.SignaturesCount += 1L;
			if (recordContext == null)
			{
				return null;
			}
			object obj = SqlClr.ObjectManager.GetObject(signatureGeneratorHandle.Value);
			if (obj is SqlClrFuzzyIndex)
			{
				obj = (obj as SqlClrFuzzyIndex).Lookups[0].SignatureGenerator;
			}
			if (obj is SignatureGenerator)
			{
				SignatureGenerator signatureGenerator = obj as SignatureGenerator;
				TemporalHandle instance = signatureGenerator.ObjectPool.GetInstance(SqlClr.ObjectManager);
				SignatureGeneratorContext signatureGeneratorContext = instance.GetObject() as SignatureGeneratorContext;
				signatureGeneratorContext.RecordContext.Read(new BinaryReader(new MemoryStream(recordContext)), signatureGeneratorContext.intAllocator, signatureGeneratorContext.byteAllocator);
				List<long> list = new List<long>();
				signatureGeneratorContext.ComputeSignatures(signatureGeneratorContext.RecordContext.TokenSequence, signatureGeneratorContext.RecordContext.TransformationMatchList, list);
				signatureGenerator.ObjectPool.ReturnInstance(instance);
				return list;
			}
			throw new Exception("Handle must be to either a FuzzyIndex or SignatureGenerator object.");
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00024B39 File Offset: 0x00022D39
		private static void FillSignaturesRow(object obj, [SqlFacet(IsNullable = false)] out long Signature)
		{
			SqlClr.s_globalStatistics.LshSignatureCount += 1L;
			Signature = (long)obj;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00024B58 File Offset: 0x00022D58
		public static SqlClrObjectManager ObjectManager
		{
			get
			{
				if (SqlClr.s_objectManager == null)
				{
					AppDomain currentDomain = AppDomain.CurrentDomain;
					lock (currentDomain)
					{
						if (SqlClr.s_objectManager == null)
						{
							SqlClr.s_objectManager = new SqlClrObjectManager();
							Interlocked.Increment(ref SqlClr.s_objectManagerCount);
							if (SqlClr.s_objectManagerCount > 1)
							{
								throw new Exception("More than one object manager was created!");
							}
						}
					}
				}
				return SqlClr.s_objectManager;
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00024BC8 File Offset: 0x00022DC8
		public static string GetSchemaName()
		{
			return SqlClr.ObjectManager.SchemaName;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00024BD4 File Offset: 0x00022DD4
		[SqlProcedure(Name = "Configure")]
		public static void Configure([SqlFacet(IsNullable = false)] SqlString command, [SqlFacet(IsNullable = true)] SqlInt32 objectHandle)
		{
			if (command.IsNullOrEmpty())
			{
				command = "get";
			}
			if (command.Value.Equals("collect", 3))
			{
				if (objectHandle.IsNull)
				{
					SqlClr.ObjectManager.Collect(true, false);
					return;
				}
				SqlClr.ObjectManager.Collect(SqlClr.ObjectManager.GetObjectHandle(objectHandle.Value).Reference, DateTime.Now);
				return;
			}
			else if (command.Value.Equals("release", 3))
			{
				if (objectHandle.IsNull)
				{
					SqlClr.ObjectManager.Collect(true, true);
					return;
				}
				SqlClr.ObjectManager.Collect(SqlClr.ObjectManager.GetObjectHandle(objectHandle.Value).Reference, DateTime.MaxValue);
				return;
			}
			else
			{
				if (command.Value.Equals("free", 3))
				{
					if (objectHandle.IsNull)
					{
						SqlClr.ObjectManager.Collect(true, true);
					}
					else
					{
						SqlClr.ObjectManager.Collect(SqlClr.ObjectManager.GetObjectHandle(objectHandle.Value).Reference, DateTime.MaxValue);
					}
					GC.Collect();
					return;
				}
				if (command.Value.StartsWith("get", 3) || command.Value.StartsWith("set", 3) || command.Value.StartsWith("xml", 3))
				{
					object obj;
					if (objectHandle.IsNull)
					{
						obj = SqlClr.ObjectManager;
					}
					else
					{
						obj = SqlClr.ObjectManager.GetObject(objectHandle.Value);
					}
					if (command.Value.Equals("get", 3))
					{
						SqlContext.Pipe.Send(string.Format("Type: {0}", obj.GetType().FullName));
						SqlContext.Pipe.Send(string.Format("Properties: ", new object[0]));
						foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties(20))
						{
							if (!propertyInfo.Name.Equals("Item") && propertyInfo.GetGetMethod().GetParameters().Length == 0)
							{
								SqlContext.Pipe.Send(string.Format("{0:50} {1}", propertyInfo.Name, propertyInfo.GetValue(obj, null).ToString()));
							}
						}
						return;
					}
					if (command.Value.StartsWith("get ", 3))
					{
						string[] array2 = command.Value.Split(new char[] { ' ' });
						PropertyInfo property = obj.GetType().GetProperty(array2[1], 20);
						if (property == null)
						{
							throw new InvalidOperationException(string.Format("Property '{0}' was not found on object of type '{1}'.", array2[1], obj.GetType().ToString()));
						}
						if (property.GetGetMethod().GetParameters().Length == 0)
						{
							SqlContext.Pipe.Send(string.Format("{0}", property.GetValue(obj, null).ToString()));
							return;
						}
					}
					else if (command.Value.StartsWith("set ", 3))
					{
						string[] array3 = command.Value.Split(new char[] { ' ' });
						PropertyInfo property2 = obj.GetType().GetProperty(array3[1]);
						if (property2 == null)
						{
							throw new InvalidOperationException(string.Format("Property '{0}' was not found on object of type '{1}'.", array3[1], obj.GetType().ToString()));
						}
						property2.SetValue(obj, ReflectionUtilities.InstantiateValue(property2.PropertyType, array3[2]), null);
						return;
					}
					else if (command.Value.Equals("xml", 3))
					{
						XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
						xmlWriterSettings.CheckCharacters = false;
						xmlWriterSettings.Indent = true;
						xmlWriterSettings.ConformanceLevel = 1;
						StringBuilder stringBuilder = new StringBuilder();
						XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(stringBuilder), xmlWriterSettings);
						if (obj is IXmlSerializable)
						{
							(obj as IXmlSerializable).WriteXml(xmlWriter);
						}
						else
						{
							xmlWriter.WriteStartElement("Object");
							xmlWriter.WriteAttributeString("typeName", obj.GetType().Name);
							xmlWriter.WriteAttributeString("assembly", obj.GetType().Assembly.FullName);
							xmlWriter.WriteStartElement("Properties");
							foreach (PropertyInfo propertyInfo2 in obj.GetType().GetProperties())
							{
								if (!propertyInfo2.Name.Equals("Item"))
								{
									xmlWriter.WriteStartElement("Property");
									xmlWriter.WriteAttributeString("name", propertyInfo2.Name);
									xmlWriter.WriteAttributeString("type", propertyInfo2.PropertyType.Name);
									xmlWriter.WriteString(propertyInfo2.GetValue(obj, null).ToString());
									xmlWriter.WriteEndElement();
								}
							}
							xmlWriter.WriteEndElement();
							xmlWriter.WriteEndElement();
						}
						xmlWriter.Flush();
						SqlContext.Pipe.Send(stringBuilder.ToString());
						return;
					}
				}
				else
				{
					if (command.Value.Equals("debug stats", 3))
					{
						SqlContext.Pipe.Send(SqlClr.s_globalStatistics.ToString());
						return;
					}
					if (command.Value.Equals("debug stats reset", 3))
					{
						SqlClr.s_globalStatistics.Clear();
						return;
					}
					if (command.Value.Equals("debug edit stats", 3))
					{
						SqlContext.Pipe.Send(EditTransformationProvider.s_globalStatistics.ToString());
						return;
					}
					if (command.Value.Equals("debug edit stats reset", 3))
					{
						EditTransformationProvider.s_globalStatistics.Reset();
						return;
					}
					if (command.Value.StartsWith("dmgr ", 3))
					{
						string[] array4 = command.Value.Split(new char[] { ' ' });
						string text = null;
						string text2 = null;
						string text3 = null;
						string text4 = null;
						string text5 = null;
						if (array4[1].StartsWith("typeName="))
						{
							text = array4[1].Split(new char[] { '=' })[1];
						}
						else if (array4[1].StartsWith("objectName="))
						{
							text2 = array4[1].Split(new char[] { '=' })[1];
						}
						if (array4[2].Contains("="))
						{
							string[] array5 = array4[2].Split(new char[] { '=' });
							text4 = array5[0];
							text5 = array5[1];
						}
						else if (array4[2].EndsWith("()"))
						{
							text3 = array4[2].Substring(0, array4[2].Length - 2);
						}
						int num = 0;
						if (objectHandle.IsNull)
						{
							using (IEnumerator<KeyValuePair<string, ObjectReference>> enumerator = SqlClr.ObjectManager.Objects().GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									KeyValuePair<string, ObjectReference> keyValuePair = enumerator.Current;
									object obj2 = keyValuePair.Value.TryGetStrongReference();
									if (obj2 is DomainManager)
									{
										DomainManager domainManager = (DomainManager)obj2;
										num += SqlClr.Apply(domainManager, text, text2, text3, text4, text5);
									}
								}
								goto IL_06C5;
							}
						}
						object @object = SqlClr.ObjectManager.GetObject(objectHandle.Value);
						if (!(@object is DomainManager))
						{
							throw new ArgumentException("The specified object must be a domain manager.");
						}
						DomainManager domainManager2 = (DomainManager)@object;
						num += SqlClr.Apply(domainManager2, text, text2, text3, text4, text5);
						IL_06C5:
						SqlContext.Pipe.Send(string.Format("Applied to {0} objects.", num));
						return;
					}
					if (command.IsNullOrEmpty() || command.Value.Equals("Help", 3) || command.Value.Equals("?", 3))
					{
						SqlContext.Pipe.Send("This procedure provides commands to manage FL3 objects.  If no @objectHandle argument is specified, the command applies to all objects and/or the global object manager.");
						SqlContext.Pipe.Send("Supported Commands: ");
						SqlContext.Pipe.Send("help or ?: prints out this list of commands.");
						SqlContext.Pipe.Send("");
						SqlContext.Pipe.Send("collect: releases all expired object handles for the specified object and converts the object to a weak reference.  If no object handle is specified, this action is applied to all objects.");
						SqlContext.Pipe.Send("release: releases all object handles for the specified object and converts the object to a weak reference.  If no object handle is specified, this action is applied to all objects.");
						SqlContext.Pipe.Send("free: calls release followed by GC.Collect().");
						SqlContext.Pipe.Send("");
						SqlContext.Pipe.Send("get [property] : prints the specified parameter value.  Prints all properties if no property name is specified.");
						SqlContext.Pipe.Send("set [property] [value]: sets the specified parameter to the given value.  Property changes will only be persisted to the object store table upon a call to Commit().");
						SqlContext.Pipe.Send("xml : prints the xml for the specified object if it implements IXmlSerializable.");
						return;
					}
					if (command.Value.EndsWith("()"))
					{
						if (objectHandle.IsNull)
						{
							throw new ArgumentNullException("Must specify an object handle to invoke a method.");
						}
						string text6 = null;
						string text7 = command.Value.Substring(0, command.Value.Length - 2);
						string text8 = null;
						string text9 = null;
						object object2 = SqlClr.ObjectManager.GetObject(objectHandle.Value);
						int num2 = SqlClr.Apply(object2, object2.GetType().Name, text6, text7, text8, text9);
						SqlContext.Pipe.Send(string.Format("Applied to {0} objects.", num2));
						return;
					}
					else
					{
						SqlContext.Pipe.Send("Unknown command.");
					}
				}
				return;
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00025454 File Offset: 0x00023654
		private static bool GetObjectName(object obj, out string name)
		{
			name = null;
			if (obj is IName)
			{
				name = (obj as IName).Name;
			}
			else
			{
				try
				{
					PropertyInfo property = obj.GetType().GetProperty("Name", 52);
					name = property.GetValue(obj, null) as string;
				}
				catch (Exception)
				{
				}
			}
			return name != null;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000254B8 File Offset: 0x000236B8
		private static int Apply(DomainManager domainManager, string typeName, string objectName, string methodName, string propertyName, string propertyValue)
		{
			int num = 0;
			foreach (string text in domainManager.DomainNames)
			{
				num += SqlClr.Apply(domainManager.GetTokenizer(text), typeName, objectName, methodName, propertyName, propertyValue);
				num += SqlClr.Apply(domainManager.GetTokenWeightProvider(text), typeName, objectName, methodName, propertyName, propertyValue);
				foreach (ITransformationProvider transformationProvider in domainManager.LeftTransformationProviders(text))
				{
					num += SqlClr.Apply(transformationProvider, typeName, objectName, methodName, propertyName, propertyValue);
				}
				foreach (ITransformationProvider transformationProvider2 in domainManager.RightTransformationProviders(text))
				{
					num += SqlClr.Apply(transformationProvider2, typeName, objectName, methodName, propertyName, propertyValue);
				}
				num += SqlClr.Apply(domainManager.GetPairSpecificTransformationProvider(text), typeName, objectName, methodName, propertyName, propertyValue);
			}
			return num;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000255DC File Offset: 0x000237DC
		private static int Apply(object obj, string typeName, string objectName, string methodName, string propertyName, string propertyValue)
		{
			int num = 0;
			string text;
			if (obj != null && ((string.IsNullOrEmpty(objectName) && string.IsNullOrEmpty(typeName)) || (!string.IsNullOrEmpty(objectName) && SqlClr.GetObjectName(obj, out text) && text.Equals(objectName)) || (!string.IsNullOrEmpty(typeName) && (obj.GetType().Name.Equals(typeName) || obj.GetType().FullName.Equals(typeName)))))
			{
				if (!string.IsNullOrEmpty(methodName))
				{
					MethodInfo method = obj.GetType().GetMethod(methodName, 52);
					if (method == null)
					{
						throw new InvalidOperationException(string.Format("Method named {0} was not found on object of type {1}.", methodName, obj.GetType().ToString()));
					}
					method.Invoke(obj, null);
					num++;
				}
				else if (!string.IsNullOrEmpty(propertyName))
				{
					PropertyInfo property = obj.GetType().GetProperty(propertyName, 52);
					if (property == null)
					{
						throw new InvalidOperationException(string.Format("Property '{0}' was not found on object of type '{1}'.", propertyName, obj.GetType().ToString()));
					}
					property.SetValue(obj, ReflectionUtilities.InstantiateValue(property.PropertyType, propertyValue), null);
					num++;
				}
			}
			if (obj is ITransformationFiltering)
			{
				ITransformationFilter transformationFilter = (obj as ITransformationFiltering).TransformationFilter;
				if (transformationFilter is TransformationFilterAggregator)
				{
					using (List<ITransformationFilter>.Enumerator enumerator = (transformationFilter as TransformationFilterAggregator).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ITransformationFilter transformationFilter2 = enumerator.Current;
							num += SqlClr.Apply(transformationFilter2, typeName, objectName, methodName, propertyName, propertyValue);
						}
						goto IL_016A;
					}
				}
				num += SqlClr.Apply(transformationFilter, typeName, objectName, methodName, propertyName, propertyValue);
			}
			IL_016A:
			if (obj is TransformationProviderAggregator)
			{
				foreach (ITransformationProvider transformationProvider in (obj as TransformationProviderAggregator).Providers)
				{
					num += SqlClr.Apply(transformationProvider, typeName, objectName, methodName, propertyName, propertyValue);
				}
			}
			if (obj is PairSpecificTransformationProviderAggregator)
			{
				foreach (IPairSpecificTransformationProvider pairSpecificTransformationProvider in (obj as PairSpecificTransformationProviderAggregator).Providers)
				{
					num += SqlClr.Apply(pairSpecificTransformationProvider, typeName, objectName, methodName, propertyName, propertyValue);
				}
			}
			return num;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002581C File Offset: 0x00023A1C
		[SqlFunction(Name = "GetTotalMemory", DataAccess = 1, SystemDataAccess = 1)]
		[return: SqlFacet(IsNullable = false)]
		public static SqlInt64 GetTotalMemory([SqlFacet(IsNullable = false)] SqlBoolean forceFullCollection)
		{
			SqlClr.ObjectManager.Collect();
			return GC.GetTotalMemory(forceFullCollection.IsTrue);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002583C File Offset: 0x00023A3C
		[SqlProcedure(Name = "CreateObject")]
		public static void CreateObject([SqlFacet(IsNullable = true)] SqlXml objectDefinition, [SqlFacet(IsNullable = true)] SqlString objectName, [SqlFacet(IsNullable = true)] SqlBoolean overwriteExisting, [SqlFacet(IsNullable = true)] SqlBoolean persist)
		{
			SqlClr.ObjectManager.Collect();
			if (overwriteExisting.IsNull)
			{
				overwriteExisting = false;
			}
			if (persist.IsNull)
			{
				persist = true;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(objectDefinition.CreateValidatingXmlReader());
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode;
			object obj;
			if ((xmlNode = xmlDocument.SelectSingleNode("/ns:Object", xmlNamespaceManager)) != null)
			{
				ObjectDefinition objectDefinition2 = new ObjectDefinition(new XmlNodeReader(xmlNode));
				if (overwriteExisting.IsFalse && SqlClr.ObjectManager.Contains(objectDefinition2.Name))
				{
					throw new Exception(string.Format("An object with name '{0}' already exists.  Call DropObject first or set @overwriteExisting to true.", objectDefinition2.Name));
				}
				obj = objectDefinition2.CreateInstance();
				objectName = objectDefinition2.Name;
			}
			else if ((xmlNode = xmlDocument.SelectSingleNode("/ns:RecordBinding", xmlNamespaceManager)) != null)
			{
				obj = new RecordBinding(new XmlNodeReader(xmlNode));
			}
			else
			{
				if ((xmlNode = xmlDocument.SelectSingleNode("/ns:SignatureGenerator", xmlNamespaceManager)) == null)
				{
					throw new ArgumentException("Must define an element of type Object, RecordBindinding or SignatureGenerator.");
				}
				obj = new SignatureGenerator(new XmlNodeReader(xmlNode));
			}
			if (objectName.IsNullOrEmpty() && obj is IName)
			{
				objectName = (obj as IName).Name;
			}
			if (objectName.IsNullOrEmpty())
			{
				throw new ArgumentException("Must define a valid objectName.");
			}
			if (obj != null)
			{
				SqlClrObjectManager objectManager = SqlClr.ObjectManager;
				lock (objectManager)
				{
					if (overwriteExisting.IsTrue && SqlClr.ObjectManager.Contains(objectName.Value))
					{
						SqlClr.ObjectManager.Drop(objectName.Value);
					}
					int num = SqlClr.ObjectManager.CreateReference(objectName.Value, obj);
					if (persist.IsTrue)
					{
						SqlClr.ObjectManager.Commit(num, objectDefinition);
					}
				}
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000259F8 File Offset: 0x00023BF8
		[SqlProcedure(Name = "CommitObject")]
		public static void CommitObject([SqlFacet(IsNullable = false)] SqlInt32 objectHandle)
		{
			if (objectHandle.IsNull)
			{
				throw new ArgumentNullException("Must specify a valid object handle");
			}
			SqlClr.ObjectManager.Commit(objectHandle.Value);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00025A1F File Offset: 0x00023C1F
		[SqlFunction(Name = "GetObjectHandle", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = false, IsPrecise = true)]
		[return: SqlFacet(IsNullable = false)]
		public static SqlInt32 GetObjectHandle([SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] SqlString objectName)
		{
			return SqlClr.ObjectManager.GetHandle(objectName.Value);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00025A38 File Offset: 0x00023C38
		[SqlFunction(Name = "GetObjectHandle2", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = false, IsPrecise = true)]
		[return: SqlFacet(IsNullable = false)]
		public static SqlInt32 GetObjectHandle2([SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] SqlString objectName, [SqlFacet(IsNullable = true)] SqlInt32 timeout)
		{
			if (timeout.IsNull)
			{
				return SqlClr.ObjectManager.GetHandle(objectName.Value);
			}
			return SqlClr.ObjectManager.GetHandle(objectName.Value, timeout.Value * 1000);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00025A88 File Offset: 0x00023C88
		[SqlFunction(Name = "GetObjectHandle3", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = false, IsPrecise = true)]
		[return: SqlFacet(IsNullable = false)]
		public static SqlInt32 GetObjectHandle3([SqlFacet(IsNullable = false, MaxSize = 4000, IsFixedLength = false)] SqlString objectName, [SqlFacet(IsNullable = true)] SqlInt32 timeout, [SqlFacet(IsNullable = true)] SqlBoolean loadOnNewConnection)
		{
			if (loadOnNewConnection.IsNull)
			{
				loadOnNewConnection = false;
			}
			if (timeout.IsNull)
			{
				timeout = SqlClr.ObjectManager.DefaultTimeout;
			}
			else
			{
				timeout = timeout.Value * 1000;
			}
			return SqlClr.ObjectManager.GetObjectHandle(objectName.Value, timeout.Value, true, loadOnNewConnection.Value).Id;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00025B00 File Offset: 0x00023D00
		[SqlFunction(Name = "Objects", FillRowMethodName = "FillObjectsRow", TableDefinition = "[Key] nvarchar(256), Name nvarchar(256), Type nvarchar(256), StronglyReferenced bit, WeaklyReferenced bit, PersistedSize bigint, LastLoaded datetime, LastAccessed datetime", DataAccess = 1, SystemDataAccess = 1)]
		public static IEnumerable Objects()
		{
			return SqlClr.ObjectManager.Objects();
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00025B0C File Offset: 0x00023D0C
		private static void FillObjectsRow(object obj, [SqlFacet(IsNullable = false, MaxSize = 256, IsFixedLength = false)] out SqlString key, [SqlFacet(IsNullable = false, MaxSize = 256, IsFixedLength = false)] out SqlString name, [SqlFacet(IsNullable = false, MaxSize = 256, IsFixedLength = false)] out SqlString type, [SqlFacet(IsNullable = false)] out SqlBoolean stronglyReferenced, [SqlFacet(IsNullable = false)] out SqlBoolean weaklyReferenced, [SqlFacet(IsNullable = true)] out SqlInt64 persistedSize, [SqlFacet(IsNullable = true)] out SqlDateTime lastLoaded, [SqlFacet(IsNullable = true)] out SqlDateTime lastAccessed)
		{
			KeyValuePair<string, ObjectReference> keyValuePair = (KeyValuePair<string, ObjectReference>)obj;
			key = keyValuePair.Key;
			ObjectReference value = keyValuePair.Value;
			name = value.Name;
			type = value.Type.FullName;
			stronglyReferenced = value.StronglyReferenced;
			weaklyReferenced = value.WeakReference != null && value.WeakReference.IsAlive;
			lastLoaded = ((value.LastLoaded == default(DateTime)) ? SqlDateTime.Null : value.LastLoaded);
			lastAccessed = ((value.LastAccessed == default(DateTime)) ? SqlDateTime.Null : value.LastAccessed);
			persistedSize = SqlInt64.Null;
			if (value.PersistedSize >= 0L)
			{
				persistedSize = value.PersistedSize;
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00025C1C File Offset: 0x00023E1C
		[SqlProcedure(Name = "DropObject")]
		public static void DropObject([SqlFacet(IsNullable = false)] SqlString objectName, [SqlFacet(IsNullable = false)] SqlBoolean dropAll, [SqlFacet(IsNullable = false)] SqlBoolean silentFail)
		{
			if (silentFail.IsNull)
			{
				silentFail = true;
			}
			SqlBoolean sqlBoolean = !dropAll;
			if (SqlBoolean.op_False(sqlBoolean) ? sqlBoolean : (sqlBoolean & objectName.IsNull))
			{
				throw new Exception("objectName may only be null if dropAll is set to true.");
			}
			try
			{
				if (dropAll)
				{
					SqlClr.ObjectManager.DropAll();
				}
				else
				{
					SqlClr.ObjectManager.Drop(objectName.Value);
				}
			}
			catch (Exception ex)
			{
				if (silentFail.IsFalse)
				{
					throw ex;
				}
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00025CB8 File Offset: 0x00023EB8
		private static DataTable InitTokenizerSchema()
		{
			DataTable dataTable = SchemaUtils.CreateSchemaTable("schema");
			DataRow dataRow = dataTable.NewRow();
			dataRow[SchemaTableColumn.ColumnName] = "1";
			dataRow[SchemaTableColumn.ColumnOrdinal] = 0;
			dataRow[SchemaTableColumn.DataType] = typeof(string);
			dataTable.Rows.Add(dataRow);
			return dataTable;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00025D18 File Offset: 0x00023F18
		public static void TokenizeFieldFillTokensRow(object obj, [SqlFacet(IsNullable = false)] out int position, out char[] token)
		{
			SqlClr.TokenizeFieldRow tokenizeFieldRow = (SqlClr.TokenizeFieldRow)obj;
			position = tokenizeFieldRow.Position;
			token = tokenizeFieldRow.Token;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00025D3C File Offset: 0x00023F3C
		[SqlFunction(Name = "TokenizeField", FillRowMethodName = "TokenizeFieldFillTokensRow", TableDefinition = "Position int, Token nvarchar(4000)", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = true)]
		public static IEnumerable TokenizeField(SqlInt32 tokenizerHandle, [SqlFacet(IsNullable = true, MaxSize = 4000, IsFixedLength = false)] char[] field)
		{
			IRecordTokenizer recordTokenizer = (IRecordTokenizer)SqlClr.ObjectManager.GetObject(tokenizerHandle.Value);
			LockFreeStack<TokenizeFieldIterator> lockFreeStack = SqlClr.ObjectManager.GetInstanceState(tokenizerHandle.Value) as LockFreeStack<TokenizeFieldIterator>;
			if (lockFreeStack == null)
			{
				SqlClrObjectManager objectManager = SqlClr.ObjectManager;
				lock (objectManager)
				{
					lockFreeStack = new LockFreeStack<TokenizeFieldIterator>();
					SqlClr.ObjectManager.SetInstanceState(tokenizerHandle.Value, lockFreeStack);
				}
			}
			TokenizeFieldIterator tokenizeFieldIterator = lockFreeStack.TryPop();
			if (tokenizeFieldIterator == null)
			{
				tokenizeFieldIterator = new TokenizeFieldIterator();
				tokenizeFieldIterator.recordTokenizer = recordTokenizer;
				tokenizeFieldIterator.TokenizerSchema = SqlClr.InitTokenizerSchema();
				tokenizeFieldIterator.simpleDataRecord = new SimpleDataRecord(tokenizeFieldIterator.TokenizerSchema, new object[1]);
				tokenizeFieldIterator.TokenIdProvider = new SessionTokenIdProvider(new NullTokenIdProvider
				{
					DomainNameList = new List<string>(new string[] { "1" })
				});
				tokenizeFieldIterator.ContextCache = lockFreeStack;
				IRecordTokenizer recordTokenizer2 = tokenizeFieldIterator.recordTokenizer;
				DataTable tokenizerSchema = tokenizeFieldIterator.TokenizerSchema;
				DomainBinding domainBinding = new DomainBinding();
				List<Column> list = new List<Column>();
				list.Add(new Column
				{
					Name = "1"
				});
				domainBinding.Columns = list;
				recordTokenizer2.Prepare(tokenizerSchema, domainBinding, out tokenizeFieldIterator.TokenizerContext);
			}
			else
			{
				tokenizeFieldIterator.Reset();
			}
			if (field != null)
			{
				tokenizeFieldIterator.simpleDataRecord[0] = new ArraySegment<char>(field);
			}
			else
			{
				tokenizeFieldIterator.simpleDataRecord[0] = string.Empty;
			}
			tokenizeFieldIterator.index = -1;
			tokenizeFieldIterator.m_tokenEnumerable = tokenizeFieldIterator.recordTokenizer.Tokenize(tokenizeFieldIterator.TokenizerContext, tokenizeFieldIterator.simpleDataRecord);
			List<SqlClr.TokenizeFieldRow> list2 = new List<SqlClr.TokenizeFieldRow>();
			IEnumerator enumerator = tokenizeFieldIterator.GetEnumerator();
			while (enumerator.MoveNext())
			{
				List<SqlClr.TokenizeFieldRow> list3 = list2;
				SqlClr.TokenizeFieldRow tokenizeFieldRow = default(SqlClr.TokenizeFieldRow);
				tokenizeFieldRow.Position = tokenizeFieldIterator.index;
				StringExtent stringExtent = tokenizeFieldIterator.m_tokenEnumerator.Current;
				tokenizeFieldRow.Token = stringExtent.ToArray();
				list3.Add(tokenizeFieldRow);
			}
			return list2;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00025F14 File Offset: 0x00024114
		public static void TokenizeRecordFillTokenizeRecordRow(object obj, [SqlFacet(IsNullable = false)] out int position, out int domainId, out int tokenId, out char[] token, out int weight)
		{
			SqlClr.TokenizeRecordRow tokenizeRecordRow = (SqlClr.TokenizeRecordRow)obj;
			position = tokenizeRecordRow.Position;
			domainId = tokenizeRecordRow.DomainId;
			tokenId = tokenizeRecordRow.TokenId;
			token = tokenizeRecordRow.Token;
			weight = tokenizeRecordRow.Weight;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00025F54 File Offset: 0x00024154
		[SqlFunction(Name = "TokenizeRecord", FillRowMethodName = "TokenizeRecordFillTokenizeRecordRow", TableDefinition = "Position int, DomainId int, TokenId int, Token nvarchar(4000), Weight int", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = true)]
		public static IEnumerable TokenizeRecord(SqlInt32 domainManagerHandle, SqlInt32 recordBindingHandle, [SqlFacet(IsNullable = true, MaxSize = 8000, IsFixedLength = false)] byte[] _record)
		{
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
			RecordBinding recordBinding = (RecordBinding)SqlClr.ObjectManager.GetObject(recordBindingHandle.Value);
			SqlClr.TokenizeRecordContextKey tokenizeRecordContextKey = new SqlClr.TokenizeRecordContextKey
			{
				domainManager = SqlClr.ObjectManager.GetObjectHandle(domainManagerHandle.Value),
				recordBinding = SqlClr.ObjectManager.GetObjectHandle(recordBindingHandle.Value)
			};
			TemporalHandle objectHandle;
			if (!SqlClr.s_TokenizeRecordContextCache.TryGetValue(tokenizeRecordContextKey, ref objectHandle))
			{
				Dictionary<SqlClr.TokenizeRecordContextKey, TemporalHandle> dictionary = SqlClr.s_TokenizeRecordContextCache;
				lock (dictionary)
				{
					if (!SqlClr.s_TokenizeRecordContextCache.TryGetValue(tokenizeRecordContextKey, ref objectHandle))
					{
						objectHandle = SqlClr.ObjectManager.GetObjectHandle(SqlClr.ObjectManager.CreateReference(new LockFreeStack<TokenizeFieldIterator>()));
						SqlClr.s_TokenizeRecordContextCache.Add(tokenizeRecordContextKey, objectHandle);
					}
				}
			}
			LockFreeStack<SqlClr.TokenizeRecordIterator> lockFreeStack = objectHandle.TryGetObject() as LockFreeStack<SqlClr.TokenizeRecordIterator>;
			if (lockFreeStack == null)
			{
				lockFreeStack = new LockFreeStack<SqlClr.TokenizeRecordIterator>();
				SqlClr.s_TokenizeRecordContextCache[tokenizeRecordContextKey] = SqlClr.ObjectManager.GetObjectHandle(SqlClr.ObjectManager.CreateReference(lockFreeStack));
			}
			SqlClr.TokenizeRecordIterator tokenizeRecordIterator = lockFreeStack.TryPop();
			if (tokenizeRecordIterator == null)
			{
				List<string> list = new List<string>(recordBinding.GetBoundDomainNames());
				TransientTokenIdProvider transientTokenIdProvider = new TransientTokenIdProvider(domainManager.TokenIdProvider);
				tokenizeRecordIterator = new SqlClr.TokenizeRecordIterator
				{
					TokenIdProvider = transientTokenIdProvider,
					LookupUpdateContext = new LookupUpdateContext(domainManager, transientTokenIdProvider, recordBinding, list, null, recordBinding.JoinSide),
					Record = new Record(),
					ContextCache = lockFreeStack
				};
			}
			else
			{
				tokenizeRecordIterator.Reset();
			}
			tokenizeRecordIterator.Record.Read(new BinaryReader(new MemoryStream(_record)));
			tokenizeRecordIterator.LookupUpdateContext.TokenizeAndWeigh(tokenizeRecordIterator.LookupUpdateContext.m_comparisonProviderInfo, tokenizeRecordIterator.LookupUpdateContext.RecordContext, tokenizeRecordIterator.Record, true);
			tokenizeRecordIterator.IterateOverTokenSequence = true;
			List<SqlClr.TokenizeRecordRow> list2 = new List<SqlClr.TokenizeRecordRow>();
			IEnumerator enumerator = tokenizeRecordIterator.GetEnumerator();
			while (enumerator.MoveNext())
			{
				TransientTokenIdProvider tokenIdProvider = tokenizeRecordIterator.TokenIdProvider;
				int num = tokenizeRecordIterator.LookupUpdateContext.RecordContext.TokenSequence.Tokens[tokenizeRecordIterator.iteratorPosition];
				ArraySegment<int> weights = tokenizeRecordIterator.LookupUpdateContext.RecordContext.TokenSequence.Weights;
				list2.Add(new SqlClr.TokenizeRecordRow
				{
					Position = tokenizeRecordIterator.iteratorPosition,
					DomainId = tokenIdProvider.GetDomainId(num),
					TokenId = num,
					Token = tokenIdProvider.GetToken(num).ToArray(),
					Weight = weights.Array[weights.Offset + tokenizeRecordIterator.iteratorPosition]
				});
			}
			return list2;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000261FC File Offset: 0x000243FC
		[SqlFunction(Name = "Transformations", FillRowMethodName = "TransformationsFillTokenizeRecordRow", TableDefinition = "Position int, DomainId int, [From] nvarchar(4000), [To] nvarchar(4000), [Type] nvarchar(4000), MetadataXml nvarchar(4000), Metadata varbinary(8000)", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = true)]
		public static IEnumerable Transformations(SqlInt32 domainManagerHandle, SqlInt32 recordBindingHandle, [SqlFacet(IsNullable = true, MaxSize = 8000, IsFixedLength = false)] byte[] _record)
		{
			DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
			RecordBinding recordBinding = (RecordBinding)SqlClr.ObjectManager.GetObject(recordBindingHandle.Value);
			Record record = new Record(new BinaryReader(new MemoryStream(_record)), HeapSegmentAllocator<char>.Instance);
			SqlClr.TokenizeRecordContextKey tokenizeRecordContextKey = new SqlClr.TokenizeRecordContextKey
			{
				domainManager = SqlClr.ObjectManager.GetObjectHandle(domainManagerHandle.Value),
				recordBinding = SqlClr.ObjectManager.GetObjectHandle(recordBindingHandle.Value)
			};
			TemporalHandle objectHandle;
			if (!SqlClr.s_TokenizeRecordContextCache.TryGetValue(tokenizeRecordContextKey, ref objectHandle))
			{
				Dictionary<SqlClr.TokenizeRecordContextKey, TemporalHandle> dictionary = SqlClr.s_TokenizeRecordContextCache;
				lock (dictionary)
				{
					if (!SqlClr.s_TokenizeRecordContextCache.TryGetValue(tokenizeRecordContextKey, ref objectHandle))
					{
						objectHandle = SqlClr.ObjectManager.GetObjectHandle(SqlClr.ObjectManager.CreateReference(new LockFreeStack<TokenizeFieldIterator>()));
						SqlClr.s_TokenizeRecordContextCache.Add(tokenizeRecordContextKey, objectHandle);
					}
				}
			}
			LockFreeStack<SqlClr.TokenizeRecordIterator> lockFreeStack = objectHandle.TryGetObject() as LockFreeStack<SqlClr.TokenizeRecordIterator>;
			if (lockFreeStack == null)
			{
				lockFreeStack = new LockFreeStack<SqlClr.TokenizeRecordIterator>();
				SqlClr.s_TokenizeRecordContextCache[tokenizeRecordContextKey] = SqlClr.ObjectManager.GetObjectHandle(SqlClr.ObjectManager.CreateReference(lockFreeStack));
			}
			SqlClr.TokenizeRecordIterator tokenizeRecordIterator = lockFreeStack.TryPop();
			if (tokenizeRecordIterator == null)
			{
				List<string> list = new List<string>(recordBinding.GetBoundDomainNames());
				tokenizeRecordIterator = new SqlClr.TokenizeRecordIterator();
				tokenizeRecordIterator.TokenIdProvider = new TransientTokenIdProvider(domainManager.TokenIdProvider);
				if (recordBinding.JoinSide == JoinSide.Undefined || recordBinding.JoinSide == JoinSide.Both)
				{
					throw new ArgumentException("RecordBinding joinSide attribute must defined and be either Left or Right.");
				}
				tokenizeRecordIterator.LookupUpdateContext = new LookupUpdateContext(domainManager, tokenizeRecordIterator.TokenIdProvider, recordBinding, list, null, recordBinding.JoinSide);
				tokenizeRecordIterator.Record = record;
				tokenizeRecordIterator.ContextCache = lockFreeStack;
			}
			else
			{
				tokenizeRecordIterator.Reset();
			}
			tokenizeRecordIterator.LookupUpdateContext.TokenizeAndRuleMatch(record);
			tokenizeRecordIterator.IterateOverTokenSequence = false;
			List<SqlClr.TransformationsRow> list2 = new List<SqlClr.TransformationsRow>();
			IEnumerator enumerator = tokenizeRecordIterator.GetEnumerator();
			TransientTokenIdProvider tokenIdProvider = tokenizeRecordIterator.TokenIdProvider;
			while (enumerator.MoveNext())
			{
				WeightedTransformationMatch weightedTransformationMatch = tokenizeRecordIterator.LookupUpdateContext.RecordContext.TransformationMatchList[tokenizeRecordIterator.iteratorPosition];
				string text = null;
				if (TransformationType.EditTransformation == weightedTransformationMatch.Transformation.Type)
				{
					EditTransformationMetadata editTransformationMetadata = new EditTransformationMetadata(weightedTransformationMatch.Transformation.Metadata);
					text = editTransformationMetadata.ToString();
				}
				else if (TransformationType.PrefixTransformation == weightedTransformationMatch.Transformation.Type)
				{
					PrefixTransformationMetadata prefixTransformationMetadata = new PrefixTransformationMetadata(weightedTransformationMatch.Transformation.Metadata);
					text = prefixTransformationMetadata.ToString();
				}
				list2.Add(new SqlClr.TransformationsRow
				{
					Position = weightedTransformationMatch.Position,
					DomainId = tokenIdProvider.GetDomainId(weightedTransformationMatch.Transformation.From[0]),
					From = weightedTransformationMatch.Transformation.From.ToVerboseString(tokenIdProvider),
					To = weightedTransformationMatch.Transformation.To.ToVerboseString(tokenIdProvider),
					Type = Enum.GetName(typeof(TransformationType), weightedTransformationMatch.Transformation.Type),
					MetadataXml = text,
					MetadataBytes = weightedTransformationMatch.Transformation.Metadata.ToArray<byte>()
				});
			}
			return list2;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00026548 File Offset: 0x00024748
		public static void TransformationsFillTokenizeRecordRow(object obj, out int position, out int domainId, out string from, out string to, out string type, out string metadataXml, out byte[] metadata)
		{
			SqlClr.TransformationsRow transformationsRow = (SqlClr.TransformationsRow)obj;
			position = transformationsRow.Position;
			domainId = transformationsRow.DomainId;
			from = transformationsRow.From;
			to = transformationsRow.To;
			type = transformationsRow.Type;
			metadataXml = transformationsRow.MetadataXml;
			metadata = transformationsRow.MetadataBytes;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00026598 File Offset: 0x00024798
		[SqlFunction(Name = "GetTokenId", DataAccess = 1, SystemDataAccess = 1, IsDeterministic = true, IsPrecise = true)]
		public static int GetTokenId(int tokenIdProviderHandle, [SqlFacet(IsNullable = true, MaxSize = 4000, IsFixedLength = false)] char[] token, int domainId)
		{
			return (SqlClr.ObjectManager.GetObject(tokenIdProviderHandle) as ITokenIdProvider).GetOrCreateTokenId(new StringExtent(token), domainId);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000265B8 File Offset: 0x000247B8
		[SqlFunction]
		public static byte[] EditTransformationMetadataBinaryFromXml(SqlString metadataXml)
		{
			XmlReader xmlReader = XmlReader.Create(new StringReader(metadataXml.Value));
			EditTransformationMetadata editTransformationMetadata = new EditTransformationMetadata(xmlReader);
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			editTransformationMetadata.Write(binaryWriter);
			binaryWriter.Flush();
			binaryWriter.BaseStream.Flush();
			return memoryStream.ToArray();
		}

		// Token: 0x0400033E RID: 830
		private static byte[] RecordContext_Empty;

		// Token: 0x0400033F RID: 831
		private static TemporalHandle s_recordContextBuilderListHandle;

		// Token: 0x04000340 RID: 832
		internal static readonly string RecordTableTypeName = "RecordTable";

		// Token: 0x04000341 RID: 833
		internal static SqlClr.SqlClrGlobalStatistics s_globalStatistics = new SqlClr.SqlClrGlobalStatistics();

		// Token: 0x04000342 RID: 834
		private static SqlClrObjectManager s_objectManager;

		// Token: 0x04000343 RID: 835
		private static int s_objectManagerCount;

		// Token: 0x04000344 RID: 836
		private static Dictionary<SqlClr.TokenizeRecordContextKey, TemporalHandle> s_TokenizeRecordContextCache = new Dictionary<SqlClr.TokenizeRecordContextKey, TemporalHandle>(SqlClr.TokenizeRecordContextKeyEqualityComparer.Instance);

		// Token: 0x0200017A RID: 378
		internal class SqlClrGlobalStatistics
		{
			// Token: 0x06000CFC RID: 3324 RVA: 0x0003792B File Offset: 0x00035B2B
			public void Clear()
			{
				this.TokenizeRecordCount = 0L;
				this.RecordContextCount = 0L;
				this.SignaturesCount = 0L;
				this.LshSignatureCount = 0L;
				this.ColumnSimilarityCount = 0L;
				this.RecordSimilarityCount = 0L;
				this.RecordContextSimilarityCount = 0L;
			}

			// Token: 0x06000CFD RID: 3325 RVA: 0x00037968 File Offset: 0x00035B68
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("{0:50} {1}", "TokenizeRecordCount", this.TokenizeRecordCount));
				stringBuilder.AppendLine(string.Format("{0:50} {1}", "RecordContextCount", this.RecordContextCount));
				stringBuilder.AppendLine(string.Format("{0:50} {1}", "SignaturesCount", this.SignaturesCount));
				stringBuilder.AppendLine(string.Format("{0:50} {1}", "LshSignatureCount", this.LshSignatureCount));
				stringBuilder.AppendLine(string.Format("{0:50} {1}", "ColumnSimilarityCount", this.ColumnSimilarityCount));
				stringBuilder.AppendLine(string.Format("{0:50} {1}", "RecordSimilarityCount", this.RecordSimilarityCount));
				stringBuilder.AppendLine(string.Format("{0:50} {1}", "RecordContextSimilarityCount", this.RecordContextSimilarityCount));
				return stringBuilder.ToString();
			}

			// Token: 0x04000603 RID: 1539
			public long TokenizeRecordCount;

			// Token: 0x04000604 RID: 1540
			public long RecordContextCount;

			// Token: 0x04000605 RID: 1541
			public long SignaturesCount;

			// Token: 0x04000606 RID: 1542
			public long LshSignatureCount;

			// Token: 0x04000607 RID: 1543
			public long ColumnSimilarityCount;

			// Token: 0x04000608 RID: 1544
			public long RecordSimilarityCount;

			// Token: 0x04000609 RID: 1545
			public long RecordContextSimilarityCount;
		}

		// Token: 0x0200017B RID: 379
		private class TokenizeRecordIterator : LockFreeStack<SqlClr.TokenizeRecordIterator>.Node, IEnumerable, IEnumerator
		{
			// Token: 0x06000CFF RID: 3327 RVA: 0x00037A6E File Offset: 0x00035C6E
			public void Reset()
			{
				this.LookupUpdateContext.Reset();
				this.TokenIdProvider.Reset();
			}

			// Token: 0x06000D00 RID: 3328 RVA: 0x00037A86 File Offset: 0x00035C86
			public IEnumerator GetEnumerator()
			{
				this.iteratorPosition = -1;
				return this;
			}

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00037A90 File Offset: 0x00035C90
			public object Current
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000D02 RID: 3330 RVA: 0x00037A94 File Offset: 0x00035C94
			public bool MoveNext()
			{
				if (this.IterateOverTokenSequence)
				{
					if (this.iteratorPosition >= this.LookupUpdateContext.RecordContext.TokenSequence.Count)
					{
						return false;
					}
					int num = this.iteratorPosition + 1;
					this.iteratorPosition = num;
					if (num == this.LookupUpdateContext.RecordContext.TokenSequence.Count)
					{
						this.ContextCache.Push(this);
						return false;
					}
				}
				else
				{
					if (this.iteratorPosition >= this.LookupUpdateContext.RecordContext.TransformationMatchList.Count)
					{
						return false;
					}
					int num = this.iteratorPosition + 1;
					this.iteratorPosition = num;
					if (num == this.LookupUpdateContext.RecordContext.TransformationMatchList.Count)
					{
						this.ContextCache.Push(this);
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000D03 RID: 3331 RVA: 0x00037B54 File Offset: 0x00035D54
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0400060A RID: 1546
			public TransientTokenIdProvider TokenIdProvider;

			// Token: 0x0400060B RID: 1547
			public LookupUpdateContext LookupUpdateContext;

			// Token: 0x0400060C RID: 1548
			public Record Record;

			// Token: 0x0400060D RID: 1549
			public int iteratorPosition;

			// Token: 0x0400060E RID: 1550
			public SqlChars sqlChars = new SqlChars(new char[4000]);

			// Token: 0x0400060F RID: 1551
			public LockFreeStack<SqlClr.TokenizeRecordIterator> ContextCache;

			// Token: 0x04000610 RID: 1552
			public bool IterateOverTokenSequence;
		}

		// Token: 0x0200017C RID: 380
		private struct TokenizeFieldRow
		{
			// Token: 0x04000611 RID: 1553
			public int Position;

			// Token: 0x04000612 RID: 1554
			public char[] Token;
		}

		// Token: 0x0200017D RID: 381
		private struct TokenizeRecordContextKey
		{
			// Token: 0x04000613 RID: 1555
			public TemporalHandle domainManager;

			// Token: 0x04000614 RID: 1556
			public TemporalHandle recordBinding;
		}

		// Token: 0x0200017E RID: 382
		private class TokenizeRecordContextKeyEqualityComparer : IEqualityComparer<SqlClr.TokenizeRecordContextKey>
		{
			// Token: 0x06000D05 RID: 3333 RVA: 0x00037B78 File Offset: 0x00035D78
			public int GetHashCode(SqlClr.TokenizeRecordContextKey k)
			{
				object obj = k.domainManager.TryGetObject();
				object obj2 = k.recordBinding.TryGetObject();
				if (obj == null || obj2 == null)
				{
					return 0;
				}
				return obj.GetHashCode() ^ obj2.GetHashCode();
			}

			// Token: 0x06000D06 RID: 3334 RVA: 0x00037BB4 File Offset: 0x00035DB4
			public bool Equals(SqlClr.TokenizeRecordContextKey k1, SqlClr.TokenizeRecordContextKey k2)
			{
				object obj = k1.domainManager.TryGetObject();
				object obj2 = k1.recordBinding.TryGetObject();
				object obj3 = k2.domainManager.TryGetObject();
				object obj4 = k2.recordBinding.TryGetObject();
				return obj != null && obj2 != null && obj3 != null && obj4 != null && obj == obj3 && obj2 == obj4;
			}

			// Token: 0x04000615 RID: 1557
			public static readonly SqlClr.TokenizeRecordContextKeyEqualityComparer Instance = new SqlClr.TokenizeRecordContextKeyEqualityComparer();
		}

		// Token: 0x0200017F RID: 383
		private struct TokenizeRecordRow
		{
			// Token: 0x04000616 RID: 1558
			public int Position;

			// Token: 0x04000617 RID: 1559
			public int DomainId;

			// Token: 0x04000618 RID: 1560
			public int TokenId;

			// Token: 0x04000619 RID: 1561
			public char[] Token;

			// Token: 0x0400061A RID: 1562
			public int Weight;
		}

		// Token: 0x02000180 RID: 384
		private struct TransformationsRow
		{
			// Token: 0x0400061B RID: 1563
			public int Position;

			// Token: 0x0400061C RID: 1564
			public int DomainId;

			// Token: 0x0400061D RID: 1565
			public string From;

			// Token: 0x0400061E RID: 1566
			public string To;

			// Token: 0x0400061F RID: 1567
			public string Type;

			// Token: 0x04000620 RID: 1568
			public string MetadataXml;

			// Token: 0x04000621 RID: 1569
			public byte[] MetadataBytes;
		}
	}
}
