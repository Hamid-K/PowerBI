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
	// Token: 0x02000488 RID: 1160
	internal interface ISapBwService
	{
		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06002691 RID: 9873
		string ConnectionString { get; }

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06002692 RID: 9874
		bool EnableStructures { get; }

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06002693 RID: 9875
		DbExceptionHandler ExceptionHandler { get; }

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06002694 RID: 9876
		IEngineHost Host { get; }

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06002695 RID: 9877
		string Language { get; }

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06002696 RID: 9878
		bool MeasuresAsDbNull { get; }

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06002697 RID: 9879
		bool PreferTablesForMultipleHierarchyNodes { get; }

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06002698 RID: 9880
		IResource Resource { get; }

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06002699 RID: 9881
		bool ScaleMeasures { get; }

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x0600269A RID: 9882
		bool SupportsColumnFolding { get; }

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x0600269B RID: 9883
		bool SupportsEnhancedMetadata { get; }

		// Token: 0x0600269C RID: 9884
		IDataReaderWithTableSchema ExecuteMdx(string mdx, RowRange range, bool cacheResults, bool newConnection = false, object[][] columnInfos = null, string cubeName = null);

		// Token: 0x0600269D RID: 9885
		IDataReaderWithTableSchema ExtractMetadata(string bapiName, string bapiReturnTable, SapBwRestrictions restrictions);

		// Token: 0x0600269E RID: 9886
		IEnumerable<MdxCellPropertyMetadata> GetCellProperties();

		// Token: 0x0600269F RID: 9887
		ILookup<string, SapBwVariable> GroupVariablesForAdditionalMetadata(SapBwMdxCube cube, Dictionary<string, SapBwVariable> variables);

		// Token: 0x060026A0 RID: 9888
		void TestConnection();

		// Token: 0x060026A1 RID: 9889
		bool TryExtractTable(string traceInfo, SapBwMetadataAstCreator astCreator, out IDataReaderWithTableSchema reader);

		// Token: 0x060026A2 RID: 9890
		bool TryGetDataReaderFromCache(string commandText, out IDataReaderWithTableSchema reader);

		// Token: 0x060026A3 RID: 9891
		bool TryGetInfoObjectsDetail(string[] infoObjects, out IDataReaderWithTableSchema reader);
	}
}
