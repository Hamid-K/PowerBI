using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004E6 RID: 1254
	internal sealed class SapBwSapClientService : ISapBwService
	{
		// Token: 0x060028E2 RID: 10466 RVA: 0x0007A2F3 File Offset: 0x000784F3
		public SapBwSapClientService()
		{
			this.deprecatedException = ValueException.NewDataSourceError<Message0>(Strings.SapBwSapClientDeprecated, Value.Null, null);
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x0007A311 File Offset: 0x00078511
		public string ConnectionString
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x0007A311 File Offset: 0x00078511
		public bool EnableStructures
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x0007A311 File Offset: 0x00078511
		public DbExceptionHandler ExceptionHandler
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x0007A311 File Offset: 0x00078511
		public IEngineHost Host
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x0007A311 File Offset: 0x00078511
		public string Language
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x0007A311 File Offset: 0x00078511
		public bool MeasuresAsDbNull
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x0007A311 File Offset: 0x00078511
		public bool PreferTablesForMultipleHierarchyNodes
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x0007A311 File Offset: 0x00078511
		public IResource Resource
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x0007A311 File Offset: 0x00078511
		public bool ScaleMeasures
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x0007A311 File Offset: 0x00078511
		public bool SupportsColumnFolding
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x0007A311 File Offset: 0x00078511
		public bool SupportsEnhancedMetadata
		{
			get
			{
				throw this.deprecatedException;
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0007A311 File Offset: 0x00078511
		public IDataReaderWithTableSchema ExecuteMdx(string mdx, RowRange range, bool cacheResults, bool newConnection = false, object[][] columnInfos = null, string cubeName = null)
		{
			throw this.deprecatedException;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0007A311 File Offset: 0x00078511
		public IDataReaderWithTableSchema ExtractMetadata(string bapiName, string bapiReturnTable, SapBwRestrictions restrictions)
		{
			throw this.deprecatedException;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0007A311 File Offset: 0x00078511
		public IEnumerable<MdxCellPropertyMetadata> GetCellProperties()
		{
			throw this.deprecatedException;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x0007A311 File Offset: 0x00078511
		public ILookup<string, SapBwVariable> GroupVariablesForAdditionalMetadata(SapBwMdxCube cube, Dictionary<string, SapBwVariable> variables)
		{
			throw this.deprecatedException;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x0007A311 File Offset: 0x00078511
		public void TestConnection()
		{
			throw this.deprecatedException;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x0007A311 File Offset: 0x00078511
		public bool TryExtractTable(string traceInfo, SapBwMetadataAstCreator astCreator, out IDataReaderWithTableSchema reader)
		{
			throw this.deprecatedException;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x0007A311 File Offset: 0x00078511
		public bool TryGetDataReaderFromCache(string commandText, out IDataReaderWithTableSchema reader)
		{
			throw this.deprecatedException;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x0007A311 File Offset: 0x00078511
		public bool TryGetInfoObjectsDetail(string[] infoObjects, out IDataReaderWithTableSchema reader)
		{
			throw this.deprecatedException;
		}

		// Token: 0x04001192 RID: 4498
		private readonly Exception deprecatedException;
	}
}
