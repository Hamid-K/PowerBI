using System;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200071A RID: 1818
	public enum CedarProperty
	{
		// Token: 0x040021B6 RID: 8630
		IsCedar,
		// Token: 0x040021B7 RID: 8631
		ProgID,
		// Token: 0x040021B8 RID: 8632
		DefaultTME,
		// Token: 0x040021B9 RID: 8633
		DefaultStyle,
		// Token: 0x040021BA RID: 8634
		Achitecture,
		// Token: 0x040021BB RID: 8635
		Transport,
		// Token: 0x040021BC RID: 8636
		IncludeActualSize,
		// Token: 0x040021BD RID: 8637
		IncludeMethod,
		// Token: 0x040021BE RID: 8638
		NoReply,
		// Token: 0x040021BF RID: 8639
		IncludeErrorBlock,
		// Token: 0x040021C0 RID: 8640
		OccursCountInName,
		// Token: 0x040021C1 RID: 8641
		OccursCountInIndex,
		// Token: 0x040021C2 RID: 8642
		RequestOffset,
		// Token: 0x040021C3 RID: 8643
		ReplyOffset,
		// Token: 0x040021C4 RID: 8644
		DataTable,
		// Token: 0x040021C5 RID: 8645
		BytesInSend,
		// Token: 0x040021C6 RID: 8646
		BytesInReceive,
		// Token: 0x040021C7 RID: 8647
		StartLevel,
		// Token: 0x040021C8 RID: 8648
		LevelIncrement,
		// Token: 0x040021C9 RID: 8649
		Level,
		// Token: 0x040021CA RID: 8650
		GroupItemBegin,
		// Token: 0x040021CB RID: 8651
		GroupItemEnd,
		// Token: 0x040021CC RID: 8652
		Default390TranID,
		// Token: 0x040021CD RID: 8653
		DPCProgramName = 22,
		// Token: 0x040021CE RID: 8654
		Convert390Int16,
		// Token: 0x040021CF RID: 8655
		Convert390Int32,
		// Token: 0x040021D0 RID: 8656
		Convert390Float,
		// Token: 0x040021D1 RID: 8657
		Convert390Double,
		// Token: 0x040021D2 RID: 8658
		Convert390Currency,
		// Token: 0x040021D3 RID: 8659
		Convert390DateTime,
		// Token: 0x040021D4 RID: 8660
		Convert390String,
		// Token: 0x040021D5 RID: 8661
		Convert390Bool,
		// Token: 0x040021D6 RID: 8662
		Convert390UnsignedByte,
		// Token: 0x040021D7 RID: 8663
		StringWidth,
		// Token: 0x040021D8 RID: 8664
		UseLink,
		// Token: 0x040021D9 RID: 8665
		ArrayDimensions,
		// Token: 0x040021DA RID: 8666
		Convert390Decimal,
		// Token: 0x040021DB RID: 8667
		ColumnWidth,
		// Token: 0x040021DC RID: 8668
		TMEClass,
		// Token: 0x040021DD RID: 8669
		VARTYPE,
		// Token: 0x040021DE RID: 8670
		Metadata,
		// Token: 0x040021DF RID: 8671
		RowCount,
		// Token: 0x040021E0 RID: 8672
		Lock,
		// Token: 0x040021E1 RID: 8673
		SendFillSize,
		// Token: 0x040021E2 RID: 8674
		ReceiveFillSize,
		// Token: 0x040021E3 RID: 8675
		ReturnValuePosition,
		// Token: 0x040021E4 RID: 8676
		Update,
		// Token: 0x040021E5 RID: 8677
		TPName390,
		// Token: 0x040021E6 RID: 8678
		OccursDependingName,
		// Token: 0x040021E7 RID: 8679
		OccursDependingIndex,
		// Token: 0x040021E8 RID: 8680
		IsVariableIn,
		// Token: 0x040021E9 RID: 8681
		IsVariableOut,
		// Token: 0x040021EA RID: 8682
		SendFillBefore,
		// Token: 0x040021EB RID: 8683
		ReceiveFillBefore,
		// Token: 0x040021EC RID: 8684
		MinimumBytesInSend,
		// Token: 0x040021ED RID: 8685
		MinimumBytesInReceive,
		// Token: 0x040021EE RID: 8686
		VariableSizeOptions,
		// Token: 0x040021EF RID: 8687
		Structure,
		// Token: 0x040021F0 RID: 8688
		TimeoutInMilliseconds,
		// Token: 0x040021F1 RID: 8689
		Convert390Stucture,
		// Token: 0x040021F2 RID: 8690
		IsCedarVersion2,
		// Token: 0x040021F3 RID: 8691
		DistributedLinkModel,
		// Token: 0x040021F4 RID: 8692
		ServerMode,
		// Token: 0x040021F5 RID: 8693
		IPEndPoint,
		// Token: 0x040021F6 RID: 8694
		CICSTransactionId,
		// Token: 0x040021F7 RID: 8695
		OTMATpipe,
		// Token: 0x040021F8 RID: 8696
		InitialBufferValue,
		// Token: 0x040021F9 RID: 8697
		MirrorTransaction,
		// Token: 0x040021FA RID: 8698
		DPCLibraryName = 66,
		// Token: 0x040021FB RID: 8699
		ChunkSize,
		// Token: 0x040021FC RID: 8700
		UseOldDecimal,
		// Token: 0x040021FD RID: 8701
		IsContext,
		// Token: 0x040021FE RID: 8702
		Server,
		// Token: 0x040021FF RID: 8703
		ParameterPosition,
		// Token: 0x04002200 RID: 8704
		DotNET,
		// Token: 0x04002201 RID: 8705
		REStyle,
		// Token: 0x04002202 RID: 8706
		TimeSeparator,
		// Token: 0x04002203 RID: 8707
		DateSeparator,
		// Token: 0x04002204 RID: 8708
		CodeGeneration,
		// Token: 0x04002205 RID: 8709
		Union,
		// Token: 0x04002206 RID: 8710
		TypeRestrictions,
		// Token: 0x04002207 RID: 8711
		WrapInDataSet,
		// Token: 0x04002208 RID: 8712
		IntendedUse,
		// Token: 0x04002209 RID: 8713
		IncludeMethodDPC,
		// Token: 0x0400220A RID: 8714
		IncludeErrorBlockDPC,
		// Token: 0x0400220B RID: 8715
		DotNETHostFile,
		// Token: 0x0400220C RID: 8716
		FileMapping,
		// Token: 0x0400220D RID: 8717
		HostFile,
		// Token: 0x0400220E RID: 8718
		NoExtraRows,
		// Token: 0x0400220F RID: 8719
		NoExtraColumns,
		// Token: 0x04002210 RID: 8720
		VirtualStructure,
		// Token: 0x04002211 RID: 8721
		UseTICSWorkArea,
		// Token: 0x04002212 RID: 8722
		ConversationalProgram,
		// Token: 0x04002213 RID: 8723
		DebugAssembly,
		// Token: 0x04002214 RID: 8724
		PersistServerObject,
		// Token: 0x04002215 RID: 8725
		CompatibleErrorCode,
		// Token: 0x04002216 RID: 8726
		InterfaceNamingConvention,
		// Token: 0x04002217 RID: 8727
		MQConnectionName,
		// Token: 0x04002218 RID: 8728
		MQConnectionType,
		// Token: 0x04002219 RID: 8729
		MQTransportType,
		// Token: 0x0400221A RID: 8730
		MQChannelName,
		// Token: 0x0400221B RID: 8731
		MQRequestUri,
		// Token: 0x0400221C RID: 8732
		MQReplyUri,
		// Token: 0x0400221D RID: 8733
		EditStateMachineIn,
		// Token: 0x0400221E RID: 8734
		EditStateMachineOut,
		// Token: 0x0400221F RID: 8735
		EditMaskIn,
		// Token: 0x04002220 RID: 8736
		EditMaskOut,
		// Token: 0x04002221 RID: 8737
		InNumericEditedResultLength,
		// Token: 0x04002222 RID: 8738
		OutNumericEditedResultLength,
		// Token: 0x04002223 RID: 8739
		CurrencySymbol,
		// Token: 0x04002224 RID: 8740
		IsPeriodComma,
		// Token: 0x04002225 RID: 8741
		HelpString,
		// Token: 0x04002226 RID: 8742
		AllowTruncatedRecords,
		// Token: 0x04002227 RID: 8743
		ReferencedInterfaceGuid,
		// Token: 0x04002228 RID: 8744
		OS390_CVT_I8,
		// Token: 0x04002229 RID: 8745
		OmitFromFixedArea,
		// Token: 0x0400222A RID: 8746
		OptimizedByteArrays,
		// Token: 0x0400222B RID: 8747
		FriendlyName = 128,
		// Token: 0x0400222C RID: 8748
		OriginalName,
		// Token: 0x0400222D RID: 8749
		OriginalDefinition,
		// Token: 0x0400222E RID: 8750
		IdentifyingGuid,
		// Token: 0x0400222F RID: 8751
		ReferencedScreen,
		// Token: 0x04002230 RID: 8752
		ReferencedField,
		// Token: 0x04002231 RID: 8753
		MethodPlan,
		// Token: 0x04002232 RID: 8754
		ReferencedPlanScreen,
		// Token: 0x04002233 RID: 8755
		ReferencedPlan,
		// Token: 0x04002234 RID: 8756
		ScreenState = 144,
		// Token: 0x04002235 RID: 8757
		SizeInRows,
		// Token: 0x04002236 RID: 8758
		SizeInColumns,
		// Token: 0x04002237 RID: 8759
		RecognitionMustBe,
		// Token: 0x04002238 RID: 8760
		RecognitionMustNotBe,
		// Token: 0x04002239 RID: 8761
		PositionRow = 160,
		// Token: 0x0400223A RID: 8762
		PositionColumn,
		// Token: 0x0400223B RID: 8763
		FieldLength,
		// Token: 0x0400223C RID: 8764
		AttributeByte,
		// Token: 0x0400223D RID: 8765
		ExpectedScreen = 176,
		// Token: 0x0400223E RID: 8766
		AIDKey,
		// Token: 0x0400223F RID: 8767
		AllowedPlanScreen,
		// Token: 0x04002240 RID: 8768
		PreferredStartingPlanScreen,
		// Token: 0x04002241 RID: 8769
		ConnectionStartingPlanScreen,
		// Token: 0x04002242 RID: 8770
		InsertAtCursor,
		// Token: 0x04002243 RID: 8771
		ExecutePlan,
		// Token: 0x04002244 RID: 8772
		ArraySize = 206
	}
}
