using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200008B RID: 139
	[OriginalName("TelemetryHostData")]
	public class TelemetryHostData : INotifyPropertyChanged
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x0000BC28 File Offset: 0x00009E28
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static TelemetryHostData CreateTelemetryHostData(bool isPublicBuild, bool isEnabled, int numberOfProcessors, int numberOfCores, bool isVirtualMachine, int countInstances, int count14xInstances, int count13xInstances, int count12xInstances, int count11xInstances)
		{
			return new TelemetryHostData
			{
				IsPublicBuild = isPublicBuild,
				IsEnabled = isEnabled,
				NumberOfProcessors = numberOfProcessors,
				NumberOfCores = numberOfCores,
				IsVirtualMachine = isVirtualMachine,
				CountInstances = countInstances,
				Count14xInstances = count14xInstances,
				Count13xInstances = count13xInstances,
				Count12xInstances = count12xInstances,
				Count11xInstances = count11xInstances
			};
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0000BC86 File Offset: 0x00009E86
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x0000BC8E File Offset: 0x00009E8E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Build")]
		public string Build
		{
			get
			{
				return this._Build;
			}
			set
			{
				this._Build = value;
				this.OnPropertyChanged("Build");
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000BCA2 File Offset: 0x00009EA2
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0000BCAA File Offset: 0x00009EAA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ExternalUser")]
		public string ExternalUser
		{
			get
			{
				return this._ExternalUser;
			}
			set
			{
				this._ExternalUser = value;
				this.OnPropertyChanged("ExternalUser");
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000BCBE File Offset: 0x00009EBE
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0000BCC6 File Offset: 0x00009EC6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsPublicBuild")]
		public bool IsPublicBuild
		{
			get
			{
				return this._IsPublicBuild;
			}
			set
			{
				this._IsPublicBuild = value;
				this.OnPropertyChanged("IsPublicBuild");
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000BCDA File Offset: 0x00009EDA
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0000BCE2 File Offset: 0x00009EE2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Host")]
		public string Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.OnPropertyChanged("Host");
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000BCF6 File Offset: 0x00009EF6
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0000BCFE File Offset: 0x00009EFE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HashedUserId")]
		public string HashedUserId
		{
			get
			{
				return this._HashedUserId;
			}
			set
			{
				this._HashedUserId = value;
				this.OnPropertyChanged("HashedUserId");
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000BD12 File Offset: 0x00009F12
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x0000BD1A File Offset: 0x00009F1A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("InstallationId")]
		public string InstallationId
		{
			get
			{
				return this._InstallationId;
			}
			set
			{
				this._InstallationId = value;
				this.OnPropertyChanged("InstallationId");
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000BD2E File Offset: 0x00009F2E
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0000BD36 File Offset: 0x00009F36
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsEnabled")]
		public bool IsEnabled
		{
			get
			{
				return this._IsEnabled;
			}
			set
			{
				this._IsEnabled = value;
				this.OnPropertyChanged("IsEnabled");
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0000BD4A File Offset: 0x00009F4A
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0000BD52 File Offset: 0x00009F52
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Edition")]
		public string Edition
		{
			get
			{
				return this._Edition;
			}
			set
			{
				this._Edition = value;
				this.OnPropertyChanged("Edition");
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0000BD66 File Offset: 0x00009F66
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0000BD6E File Offset: 0x00009F6E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AuthenticationTypes")]
		public string AuthenticationTypes
		{
			get
			{
				return this._AuthenticationTypes;
			}
			set
			{
				this._AuthenticationTypes = value;
				this.OnPropertyChanged("AuthenticationTypes");
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0000BD82 File Offset: 0x00009F82
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0000BD8A File Offset: 0x00009F8A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("NumberOfProcessors")]
		public int NumberOfProcessors
		{
			get
			{
				return this._NumberOfProcessors;
			}
			set
			{
				this._NumberOfProcessors = value;
				this.OnPropertyChanged("NumberOfProcessors");
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000BD9E File Offset: 0x00009F9E
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0000BDA6 File Offset: 0x00009FA6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("NumberOfCores")]
		public int NumberOfCores
		{
			get
			{
				return this._NumberOfCores;
			}
			set
			{
				this._NumberOfCores = value;
				this.OnPropertyChanged("NumberOfCores");
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000BDBA File Offset: 0x00009FBA
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000BDC2 File Offset: 0x00009FC2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsVirtualMachine")]
		public bool IsVirtualMachine
		{
			get
			{
				return this._IsVirtualMachine;
			}
			set
			{
				this._IsVirtualMachine = value;
				this.OnPropertyChanged("IsVirtualMachine");
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000BDD6 File Offset: 0x00009FD6
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0000BDDE File Offset: 0x00009FDE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MachineId")]
		public string MachineId
		{
			get
			{
				return this._MachineId;
			}
			set
			{
				this._MachineId = value;
				this.OnPropertyChanged("MachineId");
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000BDF2 File Offset: 0x00009FF2
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0000BDFA File Offset: 0x00009FFA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CountInstances")]
		public int CountInstances
		{
			get
			{
				return this._CountInstances;
			}
			set
			{
				this._CountInstances = value;
				this.OnPropertyChanged("CountInstances");
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000BE0E File Offset: 0x0000A00E
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x0000BE16 File Offset: 0x0000A016
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Count14xInstances")]
		public int Count14xInstances
		{
			get
			{
				return this._Count14xInstances;
			}
			set
			{
				this._Count14xInstances = value;
				this.OnPropertyChanged("Count14xInstances");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000BE2A File Offset: 0x0000A02A
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0000BE32 File Offset: 0x0000A032
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Count13xInstances")]
		public int Count13xInstances
		{
			get
			{
				return this._Count13xInstances;
			}
			set
			{
				this._Count13xInstances = value;
				this.OnPropertyChanged("Count13xInstances");
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0000BE46 File Offset: 0x0000A046
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0000BE4E File Offset: 0x0000A04E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Count12xInstances")]
		public int Count12xInstances
		{
			get
			{
				return this._Count12xInstances;
			}
			set
			{
				this._Count12xInstances = value;
				this.OnPropertyChanged("Count12xInstances");
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000BE62 File Offset: 0x0000A062
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0000BE6A File Offset: 0x0000A06A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Count11xInstances")]
		public int Count11xInstances
		{
			get
			{
				return this._Count11xInstances;
			}
			set
			{
				this._Count11xInstances = value;
				this.OnPropertyChanged("Count11xInstances");
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000BE7E File Offset: 0x0000A07E
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0000BE86 File Offset: 0x0000A086
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ProductSku")]
		public string ProductSku
		{
			get
			{
				return this._ProductSku;
			}
			set
			{
				this._ProductSku = value;
				this.OnPropertyChanged("ProductSku");
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000626 RID: 1574 RVA: 0x0000BE9C File Offset: 0x0000A09C
		// (remove) Token: 0x06000627 RID: 1575 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000628 RID: 1576 RVA: 0x0000BF09 File Offset: 0x0000A109
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040002AE RID: 686
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Build;

		// Token: 0x040002AF RID: 687
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ExternalUser;

		// Token: 0x040002B0 RID: 688
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsPublicBuild;

		// Token: 0x040002B1 RID: 689
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Host;

		// Token: 0x040002B2 RID: 690
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _HashedUserId;

		// Token: 0x040002B3 RID: 691
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _InstallationId;

		// Token: 0x040002B4 RID: 692
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsEnabled;

		// Token: 0x040002B5 RID: 693
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Edition;

		// Token: 0x040002B6 RID: 694
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _AuthenticationTypes;

		// Token: 0x040002B7 RID: 695
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _NumberOfProcessors;

		// Token: 0x040002B8 RID: 696
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _NumberOfCores;

		// Token: 0x040002B9 RID: 697
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsVirtualMachine;

		// Token: 0x040002BA RID: 698
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _MachineId;

		// Token: 0x040002BB RID: 699
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _CountInstances;

		// Token: 0x040002BC RID: 700
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Count14xInstances;

		// Token: 0x040002BD RID: 701
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Count13xInstances;

		// Token: 0x040002BE RID: 702
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Count12xInstances;

		// Token: 0x040002BF RID: 703
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Count11xInstances;

		// Token: 0x040002C0 RID: 704
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ProductSku;
	}
}
