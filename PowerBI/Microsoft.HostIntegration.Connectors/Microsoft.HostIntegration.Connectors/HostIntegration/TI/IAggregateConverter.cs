using System;
using Microsoft.HostIntegration.Common;
using Microsoft.HostIntegration.Tracing.Common;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000737 RID: 1847
	public interface IAggregateConverter
	{
		// Token: 0x060039E1 RID: 14817
		void PackServerRequest(object LibraryReader, int DispId, object[] Parameters, object OutputContainer, ref int ConvertedDataLength);

		// Token: 0x060039E2 RID: 14818
		void UnpackServerReply(object LibraryReader, int DispId, out object ReturnValue, ref object[] Parameters, object InputContainer, ref int RemainingDataLength, out bool ReadyToCommit);

		// Token: 0x060039E3 RID: 14819
		void PackClientReply(object LibraryReader, int DispId, object ReturnValue, object[] Parameters, object OutputContainer, ref int ConvertedDataLength, ref bool CanErrorBlockBeSent);

		// Token: 0x060039E4 RID: 14820
		void UnpackClientRequest(object LibraryReader, int DispId, ref object[] Parameters, object InputContainer, ref int ConvertedDataLength);

		// Token: 0x060039E5 RID: 14821
		void PackIndependentStructure(object LibraryReader, object AnnotatedCompositeItemAnnotations, object Structure, object OutputContainer, ref int ConvertedDataLength);

		// Token: 0x060039E6 RID: 14822
		void PackIndependentStructure(object LibraryReader, object AnnotatedCompositeItemAnnotations, object Structure, object OutputContainer, ref int ConvertedDataLength, int LastMemberToProcessIndex);

		// Token: 0x060039E7 RID: 14823
		void PackIndependentStructureMembers(object LibraryReader, object AnnotatedCompositeItemAnnotations, object Structure, object OutputContainer, ref int ConvertedDataLength, int StartMemberToProcessIndex, int MemberCount);

		// Token: 0x060039E8 RID: 14824
		void UnpackIndependentStructure(object LibraryReader, object AnnotatedCompositeItemAnnotations, ref object Structure, object InputContainer, ref int BufferLength);

		// Token: 0x060039E9 RID: 14825
		void PackDataTableRow(object ItemAnnotations, object DatatableItem, BufferManager IOBufferManager, ref int ConvertedDataLength, CedarProperty OffProperty);

		// Token: 0x060039EA RID: 14826
		void UnpackDataTableRow(object ItemAnnotations, int DispId, ref object DatatableItemOrDataSet, BufferManager IOBufferManager, int RowDataLength, bool IsDataVariableSize);

		// Token: 0x060039EB RID: 14827
		void Init(object RuntimeCallContext);

		// Token: 0x060039EC RID: 14828
		void SizeOfRemoteType(DataType DataType, CEDAR_TYPE_ENCODING encodingType, out int ConvertedLength);

		// Token: 0x060039ED RID: 14829
		void SizeOfRemoteType(object ItemAnnotations, ParameterDirection Direction, DataType ItemDataType, CEDAR_TYPE_ENCODING encodingType, out int ConvertedLength);

		// Token: 0x060039EE RID: 14830
		void SetCodePage(int CodePage);

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060039EF RID: 14831
		// (set) Token: 0x060039F0 RID: 14832
		AggregateConverterTracePoint TracePoint { get; set; }
	}
}
