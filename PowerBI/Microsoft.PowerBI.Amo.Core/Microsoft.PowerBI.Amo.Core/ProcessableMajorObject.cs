using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000AA RID: 170
	[Guid("922D3056-3E96-45ee-834D-DB48DDE8F7AF")]
	public abstract class ProcessableMajorObject : MajorObject, IProcessable
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00027CAA File Offset: 0x00025EAA
		DateTime IProcessable.LastProcessed
		{
			get
			{
				return this.LastProcessed;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00027CB2 File Offset: 0x00025EB2
		AnalysisState IProcessable.State
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00027CBC File Offset: 0x00025EBC
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			ProcessableMajorObject processableMajorObject = destination as ProcessableMajorObject;
			if (processableMajorObject == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
			processableMajorObject.LastProcessed = this.LastProcessed;
			processableMajorObject.State = this.State;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00027D11 File Offset: 0x00025F11
		protected ProcessableMajorObject()
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00027D2B File Offset: 0x00025F2B
		protected ProcessableMajorObject(string name)
			: base(name)
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00027D46 File Offset: 0x00025F46
		protected ProcessableMajorObject(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00027D62 File Offset: 0x00025F62
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x00027D6A File Offset: 0x00025F6A
		[ReadOnly(true)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_ProcessableMajorObject_LastProcessed")]
		public DateTime LastProcessed
		{
			get
			{
				return this.lastProcessed;
			}
			set
			{
				this.lastProcessed = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00027D73 File Offset: 0x00025F73
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00027D7B File Offset: 0x00025F7B
		[ReadOnly(true)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_ProcessableMajorObject_State")]
		public AnalysisState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x0600084E RID: 2126
		public abstract bool CanProcess(ProcessType processType);

		// Token: 0x0600084F RID: 2127 RVA: 0x00027D84 File Offset: 0x00025F84
		public void Process()
		{
			this.Process(ProcessType.ProcessDefault, null, null);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00027D8F File Offset: 0x00025F8F
		public void Process(ProcessType processType)
		{
			this.Process(processType, null, null);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00027D9A File Offset: 0x00025F9A
		public void Process(ProcessType processType, ErrorConfiguration errorConfiguration)
		{
			this.Process(processType, errorConfiguration, null);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00027DA5 File Offset: 0x00025FA5
		public void Process(ProcessType processType, WriteBackTableCreation writebackOption)
		{
			this.Process(processType, null, writebackOption, null);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00027DB1 File Offset: 0x00025FB1
		public void Process(ProcessType processType, ErrorConfiguration errorConfiguration, XmlaWarningCollection warnings)
		{
			if (!this.CanProcess(processType))
			{
				throw new InvalidOperationException(SR.InvalidProcessType(processType.ToString(), base.GetType().Name));
			}
			Server.SendProcess((IMajorObject)this, processType, null, errorConfiguration, WriteBackTableCreation.UseExisting, warnings, null, false);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00027DF1 File Offset: 0x00025FF1
		public void Process(ProcessType processType, ErrorConfiguration errorConfiguration, XmlaWarningCollection warnings, ImpactDetailCollection impactResult)
		{
			this.Process(processType, errorConfiguration, warnings, impactResult, false);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00027E00 File Offset: 0x00026000
		public void Process(ProcessType processType, ErrorConfiguration errorConfiguration, XmlaWarningCollection warnings, ImpactDetailCollection impactResult, bool analyzeImpactOnly)
		{
			if (!this.CanProcess(processType))
			{
				throw new InvalidOperationException(SR.InvalidProcessType(processType.ToString(), base.GetType().Name));
			}
			if (impactResult == null)
			{
				throw new ArgumentNullException("impactResult");
			}
			Server.SendProcess((IMajorObject)this, processType, null, errorConfiguration, WriteBackTableCreation.UseExisting, warnings, impactResult, analyzeImpactOnly);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00027E5C File Offset: 0x0002605C
		private void Process(ProcessType type, ErrorConfiguration errorConfig, WriteBackTableCreation writebackOption, XmlaWarningCollection warnings)
		{
			if (!this.CanProcess(type))
			{
				throw new InvalidOperationException(SR.InvalidProcessType(type.ToString(), base.GetType().Name));
			}
			Server.SendProcess((IMajorObject)this, type, null, errorConfig, writebackOption, warnings, null, false);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00027EA8 File Offset: 0x000260A8
		public void Process(ProcessType type, IBinding source)
		{
			if (!this.CanProcess(type))
			{
				throw new InvalidOperationException(SR.InvalidProcessType(type.ToString(), base.GetType().Name));
			}
			Server.SendProcess((IMajorObject)this, type, source, null, WriteBackTableCreation.UseExisting, null, null, false);
		}

		// Token: 0x040004B6 RID: 1206
		private DateTime lastProcessed = DateTime.MinValue;

		// Token: 0x040004B7 RID: 1207
		private AnalysisState state = AnalysisState.Unprocessed;

		// Token: 0x0200019B RID: 411
		internal abstract class ProcessableMajorObjectBody : MajorObject.MajorObjectBody
		{
			// Token: 0x06001307 RID: 4871 RVA: 0x00043160 File Offset: 0x00041360
			private protected ProcessableMajorObjectBody(ProcessableMajorObject owner)
				: base(owner)
			{
			}
		}
	}
}
