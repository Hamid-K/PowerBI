using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000063 RID: 99
	[Key("Id")]
	[OriginalName("Telemetry")]
	public class Telemetry : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x0000973F File Offset: 0x0000793F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Telemetry CreateTelemetry(Guid ID)
		{
			return new Telemetry
			{
				Id = ID
			};
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000974D File Offset: 0x0000794D
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00009755 File Offset: 0x00007955
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.OnPropertyChanged("Id");
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00009769 File Offset: 0x00007969
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00009771 File Offset: 0x00007971
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Properties")]
		public TelemetryHostData Properties
		{
			get
			{
				return this._Properties;
			}
			set
			{
				this._Properties = value;
				this.OnPropertyChanged("Properties");
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600046C RID: 1132 RVA: 0x00009788 File Offset: 0x00007988
		// (remove) Token: 0x0600046D RID: 1133 RVA: 0x000097C0 File Offset: 0x000079C0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600046E RID: 1134 RVA: 0x000097F5 File Offset: 0x000079F5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000211 RID: 529
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000212 RID: 530
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private TelemetryHostData _Properties;
	}
}
