using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E6 RID: 230
	[Key("Id")]
	[OriginalName("Telemetry")]
	public class Telemetry : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000A36 RID: 2614 RVA: 0x00014803 File Offset: 0x00012A03
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Telemetry CreateTelemetry(Guid ID)
		{
			return new Telemetry
			{
				Id = ID
			};
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00014811 File Offset: 0x00012A11
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x00014819 File Offset: 0x00012A19
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

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0001482D File Offset: 0x00012A2D
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00014835 File Offset: 0x00012A35
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

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06000A3B RID: 2619 RVA: 0x0001484C File Offset: 0x00012A4C
		// (remove) Token: 0x06000A3C RID: 2620 RVA: 0x00014884 File Offset: 0x00012A84
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A3D RID: 2621 RVA: 0x000148B9 File Offset: 0x00012AB9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004B5 RID: 1205
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040004B6 RID: 1206
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private TelemetryHostData _Properties;
	}
}
