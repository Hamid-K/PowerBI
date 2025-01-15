using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C9 RID: 969
	[BlockServiceProvider(typeof(IOnBehalfOfEventService))]
	public sealed class InProcessOnBehalfOfEventService : Block, IOnBehalfOfEventService
	{
		// Token: 0x06001DF4 RID: 7668 RVA: 0x0007174B File Offset: 0x0006F94B
		public InProcessOnBehalfOfEventService()
			: this(typeof(OnBehalfOfEventService).Name)
		{
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00010777 File Offset: 0x0000E977
		public InProcessOnBehalfOfEventService(string name)
			: base(name)
		{
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00071764 File Offset: 0x0006F964
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = BlockInitializationStatus.PartiallyDone;
			if (base.OnInitialize() == BlockInitializationStatus.Done)
			{
				this.m_onBehalfOfEventsKit = this.m_eventsKitFactory.CreateEventsKit<IOnBehalfOfEventsKit>();
				blockInitializationStatus = BlockInitializationStatus.Done;
			}
			return blockInitializationStatus;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00071790 File Offset: 0x0006F990
		public void FireOnBehalfOfEvent([NotNull] string source, [NotNull] EtwEvent etwEvent)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(source, "source");
			ExtendedDiagnostics.EnsureArgumentNotNull<EtwEvent>(etwEvent, "etwEvent");
			TraceSourceBase<EventingTrace>.Tracer.TraceVerbose("Firing '{0}' EtwEvent on behalf of {1}.", new object[] { etwEvent.EventName, source });
			string text = etwEvent.ToJsonString();
			if (EtwEvent.IsJsonEventExceedSizeLimit(text))
			{
				EtwEvent.FireTruncatedJsonEvent(this.m_onBehalfOfEventsKit, etwEvent);
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning("The EtwEvent '{0}' on behalf of '{1}' exceeds the maximum size limit of 32 KB characters and is fired with truncated payload to fit within the size limit", new object[] { etwEvent.EventName, source });
				return;
			}
			this.m_onBehalfOfEventsKit.FireOnBehalfOfJsonEvent(text);
		}

		// Token: 0x04000A46 RID: 2630
		private IOnBehalfOfEventsKit m_onBehalfOfEventsKit;

		// Token: 0x04000A47 RID: 2631
		[BlockServiceDependency]
		private IEventsKitFactory m_eventsKitFactory;
	}
}
