using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B8 RID: 9144
	[ChildElementInfo(typeof(SeekEventRecord), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NullEventRecord), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TriggerEventRecord), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PlayEventRecord), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(StopEventRecord), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PauseEventRecord), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ResumeEventRecord), FileFormatVersions.Office2010)]
	internal class ShowEventRecordList : OpenXmlCompositeElement
	{
		// Token: 0x17004C7F RID: 19583
		// (get) Token: 0x0601090D RID: 67853 RVA: 0x002E4C35 File Offset: 0x002E2E35
		public override string LocalName
		{
			get
			{
				return "showEvtLst";
			}
		}

		// Token: 0x17004C80 RID: 19584
		// (get) Token: 0x0601090E RID: 67854 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C81 RID: 19585
		// (get) Token: 0x0601090F RID: 67855 RVA: 0x002E4C3C File Offset: 0x002E2E3C
		internal override int ElementTypeId
		{
			get
			{
				return 12798;
			}
		}

		// Token: 0x06010910 RID: 67856 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010911 RID: 67857 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShowEventRecordList()
		{
		}

		// Token: 0x06010912 RID: 67858 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShowEventRecordList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010913 RID: 67859 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShowEventRecordList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010914 RID: 67860 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShowEventRecordList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010915 RID: 67861 RVA: 0x002E4C44 File Offset: 0x002E2E44
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "triggerEvt" == name)
			{
				return new TriggerEventRecord();
			}
			if (49 == namespaceId && "playEvt" == name)
			{
				return new PlayEventRecord();
			}
			if (49 == namespaceId && "stopEvt" == name)
			{
				return new StopEventRecord();
			}
			if (49 == namespaceId && "pauseEvt" == name)
			{
				return new PauseEventRecord();
			}
			if (49 == namespaceId && "resumeEvt" == name)
			{
				return new ResumeEventRecord();
			}
			if (49 == namespaceId && "seekEvt" == name)
			{
				return new SeekEventRecord();
			}
			if (49 == namespaceId && "nullEvt" == name)
			{
				return new NullEventRecord();
			}
			return null;
		}

		// Token: 0x17004C82 RID: 19586
		// (get) Token: 0x06010916 RID: 67862 RVA: 0x002E4CFA File Offset: 0x002E2EFA
		internal override string[] ElementTagNames
		{
			get
			{
				return ShowEventRecordList.eleTagNames;
			}
		}

		// Token: 0x17004C83 RID: 19587
		// (get) Token: 0x06010917 RID: 67863 RVA: 0x002E4D01 File Offset: 0x002E2F01
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShowEventRecordList.eleNamespaceIds;
			}
		}

		// Token: 0x17004C84 RID: 19588
		// (get) Token: 0x06010918 RID: 67864 RVA: 0x0000240C File Offset: 0x0000060C
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneAll;
			}
		}

		// Token: 0x17004C85 RID: 19589
		// (get) Token: 0x06010919 RID: 67865 RVA: 0x002E4D08 File Offset: 0x002E2F08
		// (set) Token: 0x0601091A RID: 67866 RVA: 0x002E4D11 File Offset: 0x002E2F11
		public TriggerEventRecord TriggerEventRecord
		{
			get
			{
				return base.GetElement<TriggerEventRecord>(0);
			}
			set
			{
				base.SetElement<TriggerEventRecord>(0, value);
			}
		}

		// Token: 0x17004C86 RID: 19590
		// (get) Token: 0x0601091B RID: 67867 RVA: 0x002E4D1B File Offset: 0x002E2F1B
		// (set) Token: 0x0601091C RID: 67868 RVA: 0x002E4D24 File Offset: 0x002E2F24
		public PlayEventRecord PlayEventRecord
		{
			get
			{
				return base.GetElement<PlayEventRecord>(1);
			}
			set
			{
				base.SetElement<PlayEventRecord>(1, value);
			}
		}

		// Token: 0x17004C87 RID: 19591
		// (get) Token: 0x0601091D RID: 67869 RVA: 0x002E4D2E File Offset: 0x002E2F2E
		// (set) Token: 0x0601091E RID: 67870 RVA: 0x002E4D37 File Offset: 0x002E2F37
		public StopEventRecord StopEventRecord
		{
			get
			{
				return base.GetElement<StopEventRecord>(2);
			}
			set
			{
				base.SetElement<StopEventRecord>(2, value);
			}
		}

		// Token: 0x17004C88 RID: 19592
		// (get) Token: 0x0601091F RID: 67871 RVA: 0x002E4D41 File Offset: 0x002E2F41
		// (set) Token: 0x06010920 RID: 67872 RVA: 0x002E4D4A File Offset: 0x002E2F4A
		public PauseEventRecord PauseEventRecord
		{
			get
			{
				return base.GetElement<PauseEventRecord>(3);
			}
			set
			{
				base.SetElement<PauseEventRecord>(3, value);
			}
		}

		// Token: 0x17004C89 RID: 19593
		// (get) Token: 0x06010921 RID: 67873 RVA: 0x002E4D54 File Offset: 0x002E2F54
		// (set) Token: 0x06010922 RID: 67874 RVA: 0x002E4D5D File Offset: 0x002E2F5D
		public ResumeEventRecord ResumeEventRecord
		{
			get
			{
				return base.GetElement<ResumeEventRecord>(4);
			}
			set
			{
				base.SetElement<ResumeEventRecord>(4, value);
			}
		}

		// Token: 0x17004C8A RID: 19594
		// (get) Token: 0x06010923 RID: 67875 RVA: 0x002E4D67 File Offset: 0x002E2F67
		// (set) Token: 0x06010924 RID: 67876 RVA: 0x002E4D70 File Offset: 0x002E2F70
		public SeekEventRecord SeekEventRecord
		{
			get
			{
				return base.GetElement<SeekEventRecord>(5);
			}
			set
			{
				base.SetElement<SeekEventRecord>(5, value);
			}
		}

		// Token: 0x17004C8B RID: 19595
		// (get) Token: 0x06010925 RID: 67877 RVA: 0x002E4D7A File Offset: 0x002E2F7A
		// (set) Token: 0x06010926 RID: 67878 RVA: 0x002E4D83 File Offset: 0x002E2F83
		public NullEventRecord NullEventRecord
		{
			get
			{
				return base.GetElement<NullEventRecord>(6);
			}
			set
			{
				base.SetElement<NullEventRecord>(6, value);
			}
		}

		// Token: 0x06010927 RID: 67879 RVA: 0x002E4D8D File Offset: 0x002E2F8D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowEventRecordList>(deep);
		}

		// Token: 0x0400754B RID: 30027
		private const string tagName = "showEvtLst";

		// Token: 0x0400754C RID: 30028
		private const byte tagNsId = 49;

		// Token: 0x0400754D RID: 30029
		internal const int ElementTypeIdConst = 12798;

		// Token: 0x0400754E RID: 30030
		private static readonly string[] eleTagNames = new string[] { "triggerEvt", "playEvt", "stopEvt", "pauseEvt", "resumeEvt", "seekEvt", "nullEvt" };

		// Token: 0x0400754F RID: 30031
		private static readonly byte[] eleNamespaceIds = new byte[] { 49, 49, 49, 49, 49, 49, 49 };
	}
}
