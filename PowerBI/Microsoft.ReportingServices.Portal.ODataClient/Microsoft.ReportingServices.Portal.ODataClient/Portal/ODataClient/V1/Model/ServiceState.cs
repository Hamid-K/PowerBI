using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E9 RID: 233
	[OriginalName("ServiceState")]
	public class ServiceState : INotifyPropertyChanged
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x000149E5 File Offset: 0x00012BE5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ServiceState CreateServiceState(bool isAvailable, bool userHasFavorites, bool requireIntune)
		{
			return new ServiceState
			{
				IsAvailable = isAvailable,
				UserHasFavorites = userHasFavorites,
				RequireIntune = requireIntune
			};
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00014A01 File Offset: 0x00012C01
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x00014A09 File Offset: 0x00012C09
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsAvailable")]
		public bool IsAvailable
		{
			get
			{
				return this._IsAvailable;
			}
			set
			{
				this._IsAvailable = value;
				this.OnPropertyChanged("IsAvailable");
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00014A1D File Offset: 0x00012C1D
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00014A25 File Offset: 0x00012C25
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("RestrictedFeatures")]
		public ObservableCollection<string> RestrictedFeatures
		{
			get
			{
				return this._RestrictedFeatures;
			}
			set
			{
				this._RestrictedFeatures = value;
				this.OnPropertyChanged("RestrictedFeatures");
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00014A39 File Offset: 0x00012C39
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00014A41 File Offset: 0x00012C41
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AllowedSystemActions")]
		public ObservableCollection<string> AllowedSystemActions
		{
			get
			{
				return this._AllowedSystemActions;
			}
			set
			{
				this._AllowedSystemActions = value;
				this.OnPropertyChanged("AllowedSystemActions");
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00014A55 File Offset: 0x00012C55
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00014A5D File Offset: 0x00012C5D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("TimeZone")]
		public string TimeZone
		{
			get
			{
				return this._TimeZone;
			}
			set
			{
				this._TimeZone = value;
				this.OnPropertyChanged("TimeZone");
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00014A71 File Offset: 0x00012C71
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00014A79 File Offset: 0x00012C79
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("UserHasFavorites")]
		public bool UserHasFavorites
		{
			get
			{
				return this._UserHasFavorites;
			}
			set
			{
				this._UserHasFavorites = value;
				this.OnPropertyChanged("UserHasFavorites");
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00014A8D File Offset: 0x00012C8D
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x00014A95 File Offset: 0x00012C95
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AcceptLanguage")]
		public string AcceptLanguage
		{
			get
			{
				return this._AcceptLanguage;
			}
			set
			{
				this._AcceptLanguage = value;
				this.OnPropertyChanged("AcceptLanguage");
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00014AA9 File Offset: 0x00012CA9
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00014AB1 File Offset: 0x00012CB1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("RequireIntune")]
		public bool RequireIntune
		{
			get
			{
				return this._RequireIntune;
			}
			set
			{
				this._RequireIntune = value;
				this.OnPropertyChanged("RequireIntune");
			}
		}

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06000A5C RID: 2652 RVA: 0x00014AC8 File Offset: 0x00012CC8
		// (remove) Token: 0x06000A5D RID: 2653 RVA: 0x00014B00 File Offset: 0x00012D00
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A5E RID: 2654 RVA: 0x00014B35 File Offset: 0x00012D35
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004BC RID: 1212
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsAvailable;

		// Token: 0x040004BD RID: 1213
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _RestrictedFeatures = new ObservableCollection<string>();

		// Token: 0x040004BE RID: 1214
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _AllowedSystemActions = new ObservableCollection<string>();

		// Token: 0x040004BF RID: 1215
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _TimeZone;

		// Token: 0x040004C0 RID: 1216
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _UserHasFavorites;

		// Token: 0x040004C1 RID: 1217
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _AcceptLanguage;

		// Token: 0x040004C2 RID: 1218
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _RequireIntune;
	}
}
