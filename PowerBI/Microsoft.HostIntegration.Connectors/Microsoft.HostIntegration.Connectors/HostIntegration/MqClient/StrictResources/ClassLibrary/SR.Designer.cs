using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary
{
	// Token: 0x02000B5D RID: 2909
	internal class SR
	{
		// Token: 0x06005C85 RID: 23685 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06005C86 RID: 23686 RVA: 0x0017D5E4 File Offset: 0x0017B7E4
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06005C87 RID: 23687 RVA: 0x0017D610 File Offset: 0x0017B810
		// (set) Token: 0x06005C88 RID: 23688 RVA: 0x0017D617 File Offset: 0x0017B817
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06005C89 RID: 23689 RVA: 0x0017D61F File Offset: 0x0017B81F
		internal static string QueueNameReadonlyAfterOpen
		{
			get
			{
				return SR.ResourceManager.GetString("QueueNameReadonlyAfterOpen", SR.Culture);
			}
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06005C8A RID: 23690 RVA: 0x0017D635 File Offset: 0x0017B835
		internal static string QueueManagerReadonlyAfterOpen
		{
			get
			{
				return SR.ResourceManager.GetString("QueueManagerReadonlyAfterOpen", SR.Culture);
			}
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06005C8B RID: 23691 RVA: 0x0017D64B File Offset: 0x0017B84B
		internal static string QueueAlreadyOpened
		{
			get
			{
				return SR.ResourceManager.GetString("QueueAlreadyOpened", SR.Culture);
			}
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06005C8C RID: 23692 RVA: 0x0017D661 File Offset: 0x0017B861
		internal static string OpenWithNoQueueManager
		{
			get
			{
				return SR.ResourceManager.GetString("OpenWithNoQueueManager", SR.Culture);
			}
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06005C8D RID: 23693 RVA: 0x0017D677 File Offset: 0x0017B877
		internal static string OpenSendWithShared
		{
			get
			{
				return SR.ResourceManager.GetString("OpenSendWithShared", SR.Culture);
			}
		}

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06005C8E RID: 23694 RVA: 0x0017D68D File Offset: 0x0017B88D
		internal static string QueueNotOpened
		{
			get
			{
				return SR.ResourceManager.GetString("QueueNotOpened", SR.Culture);
			}
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06005C8F RID: 23695 RVA: 0x0017D6A3 File Offset: 0x0017B8A3
		internal static string QueueNotOpenedForSend
		{
			get
			{
				return SR.ResourceManager.GetString("QueueNotOpenedForSend", SR.Culture);
			}
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06005C90 RID: 23696 RVA: 0x0017D6B9 File Offset: 0x0017B8B9
		internal static string QueueNotOpenedForReceive
		{
			get
			{
				return SR.ResourceManager.GetString("QueueNotOpenedForReceive", SR.Culture);
			}
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06005C91 RID: 23697 RVA: 0x0017D6CF File Offset: 0x0017B8CF
		internal static string ReceiveWaitWith0Timeout
		{
			get
			{
				return SR.ResourceManager.GetString("ReceiveWaitWith0Timeout", SR.Culture);
			}
		}

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06005C92 RID: 23698 RVA: 0x0017D6E5 File Offset: 0x0017B8E5
		internal static string QueueManagerNameReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("QueueManagerNameReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06005C93 RID: 23699 RVA: 0x0017D6FB File Offset: 0x0017B8FB
		internal static string ChannelNameReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("ChannelNameReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06005C94 RID: 23700 RVA: 0x0017D711 File Offset: 0x0017B911
		internal static string HostReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("HostReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06005C95 RID: 23701 RVA: 0x0017D727 File Offset: 0x0017B927
		internal static string PortReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("PortReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06005C96 RID: 23702 RVA: 0x0017D73D File Offset: 0x0017B93D
		internal static string UseSslReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("UseSslReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06005C97 RID: 23703 RVA: 0x0017D753 File Offset: 0x0017B953
		internal static string CertificateCollectionReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("CertificateCollectionReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06005C98 RID: 23704 RVA: 0x0017D769 File Offset: 0x0017B969
		internal static string ServerCertificateThumbprintsReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("ServerCertificateThumbprintsReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06005C99 RID: 23705 RVA: 0x0017D77F File Offset: 0x0017B97F
		internal static string ConnectAsReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("ConnectAsReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06005C9A RID: 23706 RVA: 0x0017D795 File Offset: 0x0017B995
		internal static string AuthorizationUserReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("AuthorizationUserReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06005C9B RID: 23707 RVA: 0x0017D7AB File Offset: 0x0017B9AB
		internal static string AuthorizationPasswordReadonlyAfterConnect
		{
			get
			{
				return SR.ResourceManager.GetString("AuthorizationPasswordReadonlyAfterConnect", SR.Culture);
			}
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06005C9C RID: 23708 RVA: 0x0017D7C1 File Offset: 0x0017B9C1
		internal static string QueueManagerConnectMissingAuthorizationUser
		{
			get
			{
				return SR.ResourceManager.GetString("QueueManagerConnectMissingAuthorizationUser", SR.Culture);
			}
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06005C9D RID: 23709 RVA: 0x0017D7D7 File Offset: 0x0017B9D7
		internal static string QueueManagerAlreadyConnected
		{
			get
			{
				return SR.ResourceManager.GetString("QueueManagerAlreadyConnected", SR.Culture);
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06005C9E RID: 23710 RVA: 0x0017D7ED File Offset: 0x0017B9ED
		internal static string QueueManagerNotConnected
		{
			get
			{
				return SR.ResourceManager.GetString("QueueManagerNotConnected", SR.Culture);
			}
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06005C9F RID: 23711 RVA: 0x0017D803 File Offset: 0x0017BA03
		internal static string ReceiveQueueNotAvailableForContextState
		{
			get
			{
				return SR.ResourceManager.GetString("ReceiveQueueNotAvailableForContextState", SR.Culture);
			}
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06005CA0 RID: 23712 RVA: 0x0017D819 File Offset: 0x0017BA19
		internal static string ReceiveQueueNotAvailableForContextQm
		{
			get
			{
				return SR.ResourceManager.GetString("ReceiveQueueNotAvailableForContextQm", SR.Culture);
			}
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06005CA1 RID: 23713 RVA: 0x0017D82F File Offset: 0x0017BA2F
		internal static string QueueSendDataTooLong
		{
			get
			{
				return SR.ResourceManager.GetString("QueueSendDataTooLong", SR.Culture);
			}
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x06005CA2 RID: 23714 RVA: 0x0017D845 File Offset: 0x0017BA45
		internal static string TransactionalQNeedsTransactionalQm
		{
			get
			{
				return SR.ResourceManager.GetString("TransactionalQNeedsTransactionalQm", SR.Culture);
			}
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06005CA3 RID: 23715 RVA: 0x0017D85B File Offset: 0x0017BA5B
		internal static string NonTransactionalQNeedsNonTransactionalQm
		{
			get
			{
				return SR.ResourceManager.GetString("NonTransactionalQNeedsNonTransactionalQm", SR.Culture);
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06005CA4 RID: 23716 RVA: 0x0017D871 File Offset: 0x0017BA71
		internal static string TransactionalNotInTransaction
		{
			get
			{
				return SR.ResourceManager.GetString("TransactionalNotInTransaction", SR.Culture);
			}
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x06005CA5 RID: 23717 RVA: 0x0017D887 File Offset: 0x0017BA87
		internal static string TransactionalWrongThread
		{
			get
			{
				return SR.ResourceManager.GetString("TransactionalWrongThread", SR.Culture);
			}
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x06005CA6 RID: 23718 RVA: 0x0017D89D File Offset: 0x0017BA9D
		internal static string NonTransactionalInTransaction
		{
			get
			{
				return SR.ResourceManager.GetString("NonTransactionalInTransaction", SR.Culture);
			}
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06005CA7 RID: 23719 RVA: 0x0017D8B3 File Offset: 0x0017BAB3
		internal static string PositiveOrUnlimited
		{
			get
			{
				return SR.ResourceManager.GetString("PositiveOrUnlimited", SR.Culture);
			}
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06005CA8 RID: 23720 RVA: 0x0017D8C9 File Offset: 0x0017BAC9
		internal static string PositiveOrNoTruncation
		{
			get
			{
				return SR.ResourceManager.GetString("PositiveOrNoTruncation", SR.Culture);
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06005CA9 RID: 23721 RVA: 0x0017D8DF File Offset: 0x0017BADF
		internal static string HeaderNull
		{
			get
			{
				return SR.ResourceManager.GetString("HeaderNull", SR.Culture);
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06005CAA RID: 23722 RVA: 0x0017D8F5 File Offset: 0x0017BAF5
		internal static string FolderEmpty
		{
			get
			{
				return SR.ResourceManager.GetString("FolderEmpty", SR.Culture);
			}
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06005CAB RID: 23723 RVA: 0x0017D90B File Offset: 0x0017BB0B
		internal static string RulesAndFormattingHeaderValue
		{
			get
			{
				return SR.ResourceManager.GetString("RulesAndFormattingHeaderValue", SR.Culture);
			}
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06005CAC RID: 23724 RVA: 0x0017D921 File Offset: 0x0017BB21
		internal static string RulesAndFormattingHeaderQuotationMarks
		{
			get
			{
				return SR.ResourceManager.GetString("RulesAndFormattingHeaderQuotationMarks", SR.Culture);
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06005CAD RID: 23725 RVA: 0x0017D937 File Offset: 0x0017BB37
		internal static string RulesAndFormattingHeaderSingleEndInvalid
		{
			get
			{
				return SR.ResourceManager.GetString("RulesAndFormattingHeaderSingleEndInvalid", SR.Culture);
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06005CAE RID: 23726 RVA: 0x0017D94D File Offset: 0x0017BB4D
		internal static string RulesAndFormattingHeaderUnescapedQuotationMarks
		{
			get
			{
				return SR.ResourceManager.GetString("RulesAndFormattingHeaderUnescapedQuotationMarks", SR.Culture);
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06005CAF RID: 23727 RVA: 0x0017D963 File Offset: 0x0017BB63
		internal static string StringListLengths
		{
			get
			{
				return SR.ResourceManager.GetString("StringListLengths", SR.Culture);
			}
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06005CB0 RID: 23728 RVA: 0x0017D979 File Offset: 0x0017BB79
		internal static string OpenDoesntAllowReceiveWithoutOptions
		{
			get
			{
				return SR.ResourceManager.GetString("OpenDoesntAllowReceiveWithoutOptions", SR.Culture);
			}
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06005CB1 RID: 23729 RVA: 0x0017D98F File Offset: 0x0017BB8F
		internal static string CicsImsHeaders
		{
			get
			{
				return SR.ResourceManager.GetString("CicsImsHeaders", SR.Culture);
			}
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06005CB2 RID: 23730 RVA: 0x0017D9A5 File Offset: 0x0017BBA5
		internal static string FolderContentsNotType
		{
			get
			{
				return SR.ResourceManager.GetString("FolderContentsNotType", SR.Culture);
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06005CB3 RID: 23731 RVA: 0x0017D9BB File Offset: 0x0017BBBB
		internal static string FolderContentsNotTag
		{
			get
			{
				return SR.ResourceManager.GetString("FolderContentsNotTag", SR.Culture);
			}
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06005CB4 RID: 23732 RVA: 0x0017D9D1 File Offset: 0x0017BBD1
		internal static string PropertyOneFolder
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyOneFolder", SR.Culture);
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06005CB5 RID: 23733 RVA: 0x0017D9E7 File Offset: 0x0017BBE7
		internal static string PropertyNotInFolder
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyNotInFolder", SR.Culture);
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06005CB6 RID: 23734 RVA: 0x0017D9FD File Offset: 0x0017BBFD
		internal static string PropertyAlreadyInFolder
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyAlreadyInFolder", SR.Culture);
			}
		}

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06005CB7 RID: 23735 RVA: 0x0017DA13 File Offset: 0x0017BC13
		internal static string PropertyNoFolder
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyNoFolder", SR.Culture);
			}
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06005CB8 RID: 23736 RVA: 0x0017DA29 File Offset: 0x0017BC29
		internal static string FolderOneHeader
		{
			get
			{
				return SR.ResourceManager.GetString("FolderOneHeader", SR.Culture);
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06005CB9 RID: 23737 RVA: 0x0017DA3F File Offset: 0x0017BC3F
		internal static string FolderAlreadyInHeader
		{
			get
			{
				return SR.ResourceManager.GetString("FolderAlreadyInHeader", SR.Culture);
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06005CBA RID: 23738 RVA: 0x0017DA55 File Offset: 0x0017BC55
		internal static string FolderNotInHeader
		{
			get
			{
				return SR.ResourceManager.GetString("FolderNotInHeader", SR.Culture);
			}
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06005CBB RID: 23739 RVA: 0x0017DA6B File Offset: 0x0017BC6B
		internal static string FolderNoHeader
		{
			get
			{
				return SR.ResourceManager.GetString("FolderNoHeader", SR.Culture);
			}
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06005CBC RID: 23740 RVA: 0x0017DA81 File Offset: 0x0017BC81
		internal static string PropertyNotInMessage
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyNotInMessage", SR.Culture);
			}
		}

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06005CBD RID: 23741 RVA: 0x0017DA97 File Offset: 0x0017BC97
		internal static string PropertyNotFoundInMessage
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyNotFoundInMessage", SR.Culture);
			}
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06005CBE RID: 23742 RVA: 0x0017DAAD File Offset: 0x0017BCAD
		internal static string PropertyNotFoundInFolder
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyNotFoundInFolder", SR.Culture);
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06005CBF RID: 23743 RVA: 0x0017DAC3 File Offset: 0x0017BCC3
		internal static string PropertyDotted
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyDotted", SR.Culture);
			}
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06005CC0 RID: 23744 RVA: 0x0017DAD9 File Offset: 0x0017BCD9
		internal static string FolderContentsNotParseable
		{
			get
			{
				return SR.ResourceManager.GetString("FolderContentsNotParseable", SR.Culture);
			}
		}

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06005CC1 RID: 23745 RVA: 0x0017DAEF File Offset: 0x0017BCEF
		internal static string EmptyPropertyValueXml
		{
			get
			{
				return SR.ResourceManager.GetString("EmptyPropertyValueXml", SR.Culture);
			}
		}

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06005CC2 RID: 23746 RVA: 0x0017DB05 File Offset: 0x0017BD05
		internal static string UnknownBoolXml
		{
			get
			{
				return SR.ResourceManager.GetString("UnknownBoolXml", SR.Culture);
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06005CC3 RID: 23747 RVA: 0x0017DB1B File Offset: 0x0017BD1B
		internal static string MixedModeOccurs
		{
			get
			{
				return SR.ResourceManager.GetString("MixedModeOccurs", SR.Culture);
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06005CC4 RID: 23748 RVA: 0x0017DB31 File Offset: 0x0017BD31
		internal static string PropertyNameInvalid
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyNameInvalid", SR.Culture);
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06005CC5 RID: 23749 RVA: 0x0017DB47 File Offset: 0x0017BD47
		internal static string PropertyNotInJmsMcd
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyNotInJmsMcd", SR.Culture);
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06005CC6 RID: 23750 RVA: 0x0017DB5D File Offset: 0x0017BD5D
		internal static string AddPropertyWrongFolderType
		{
			get
			{
				return SR.ResourceManager.GetString("AddPropertyWrongFolderType", SR.Culture);
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06005CC7 RID: 23751 RVA: 0x0017DB73 File Offset: 0x0017BD73
		internal static string AddPropertyWrongFolderTag
		{
			get
			{
				return SR.ResourceManager.GetString("AddPropertyWrongFolderTag", SR.Culture);
			}
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x0017DB89 File Offset: 0x0017BD89
		internal static string PropertiesWithSameName
		{
			get
			{
				return SR.ResourceManager.GetString("PropertiesWithSameName", SR.Culture);
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06005CC9 RID: 23753 RVA: 0x0017DB9F File Offset: 0x0017BD9F
		internal static string PropertyNameAndValueNameDiffer
		{
			get
			{
				return SR.ResourceManager.GetString("PropertyNameAndValueNameDiffer", SR.Culture);
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x0017DBB5 File Offset: 0x0017BDB5
		internal static string UsrPropertyDotted
		{
			get
			{
				return SR.ResourceManager.GetString("UsrPropertyDotted", SR.Culture);
			}
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06005CCB RID: 23755 RVA: 0x0017DBCB File Offset: 0x0017BDCB
		internal static string MixedUsageOfRulesAndFormattingHeader
		{
			get
			{
				return SR.ResourceManager.GetString("MixedUsageOfRulesAndFormattingHeader", SR.Culture);
			}
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x0017DBE1 File Offset: 0x0017BDE1
		internal static string InvalidQueueManagerAlias(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidQueueManagerAlias", SR.Culture), param0);
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x0017DC02 File Offset: 0x0017BE02
		internal static string InvalidQueueAlias(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidQueueAlias", SR.Culture), param0);
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x0017DC23 File Offset: 0x0017BE23
		internal static string QueueManagerConnectFailed(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueManagerConnectFailed", SR.Culture), param0, param1);
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x0017DC45 File Offset: 0x0017BE45
		internal static string QueueOpenFailed(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueOpenFailed", SR.Culture), param0, param1);
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x0017DC67 File Offset: 0x0017BE67
		internal static string QueueCloseFailed(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueCloseFailed", SR.Culture), param0, param1);
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x0017DC89 File Offset: 0x0017BE89
		internal static string QueueSendFailed(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueSendFailed", SR.Culture), param0, param1);
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x0017DCAB File Offset: 0x0017BEAB
		internal static string QueueReceiveFailed(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueReceiveFailed", SR.Culture), param0, param1);
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x0017DCCD File Offset: 0x0017BECD
		internal static string QueueOpenFailedQmDisconnected(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueOpenFailedQmDisconnected", SR.Culture), param0, param1);
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x0017DCEF File Offset: 0x0017BEEF
		internal static string QueueSendFailedQmDisconnected(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueSendFailedQmDisconnected", SR.Culture), param0);
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x0017DD10 File Offset: 0x0017BF10
		internal static string QueueReceiveFailedQmDisconnected(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueReceiveFailedQmDisconnected", SR.Culture), param0);
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x0017DD31 File Offset: 0x0017BF31
		internal static string OpenQueueException(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("OpenQueueException", SR.Culture), param0);
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x0017DD52 File Offset: 0x0017BF52
		internal static string CloseQueueException(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("CloseQueueException", SR.Culture), param0);
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x0017DD73 File Offset: 0x0017BF73
		internal static string SendToQueueException(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("SendToQueueException", SR.Culture), param0);
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x0017DD94 File Offset: 0x0017BF94
		internal static string ReceiveFromQueueException(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("ReceiveFromQueueException", SR.Culture), param0);
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x0017DDB5 File Offset: 0x0017BFB5
		internal static string ConnectQueueManagerException(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("ConnectQueueManagerException", SR.Culture), param0);
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x0017DDD6 File Offset: 0x0017BFD6
		internal static string DisconnectQueueManagerException(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DisconnectQueueManagerException", SR.Culture), param0);
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x0017DDF7 File Offset: 0x0017BFF7
		internal static string UpdateAsyncCountersException(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UpdateAsyncCountersException", SR.Culture), param0);
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x0017DE18 File Offset: 0x0017C018
		internal static string UnparseableIntProperty(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnparseableIntProperty", SR.Culture), param0, param1, param2);
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x0017DE3B File Offset: 0x0017C03B
		internal static string UnparseableLongProperty(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnparseableLongProperty", SR.Culture), param0, param1, param2);
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x0017DE5E File Offset: 0x0017C05E
		internal static string FolderUnknownProperty(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("FolderUnknownProperty", SR.Culture), param0, param1);
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x0017DE80 File Offset: 0x0017C080
		internal static string CcsidNamesValuesInvalid(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("CcsidNamesValuesInvalid", SR.Culture), param0);
		}

		// Token: 0x04004865 RID: 18533
		private static ResourceManager resourceManager;

		// Token: 0x04004866 RID: 18534
		private static CultureInfo resourceCulture;
	}
}
