using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000086 RID: 134
	[OriginalName("DataSetParameterInfo")]
	public class DataSetParameterInfo : INotifyPropertyChanged
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0000B951 File Offset: 0x00009B51
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataSetParameterInfo CreateDataSetParameterInfo(bool nullable, ReportParameterType dataType, bool isExpression, bool isMultiValued)
		{
			return new DataSetParameterInfo
			{
				Nullable = nullable,
				DataType = dataType,
				IsExpression = isExpression,
				IsMultiValued = isMultiValued
			};
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000B974 File Offset: 0x00009B74
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0000B97C File Offset: 0x00009B7C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Name")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.OnPropertyChanged("Name");
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000B990 File Offset: 0x00009B90
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000B998 File Offset: 0x00009B98
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DefaultValue")]
		public string DefaultValue
		{
			get
			{
				return this._DefaultValue;
			}
			set
			{
				this._DefaultValue = value;
				this.OnPropertyChanged("DefaultValue");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000B9AC File Offset: 0x00009BAC
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Nullable")]
		public bool Nullable
		{
			get
			{
				return this._Nullable;
			}
			set
			{
				this._Nullable = value;
				this.OnPropertyChanged("Nullable");
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataType")]
		public ReportParameterType DataType
		{
			get
			{
				return this._DataType;
			}
			set
			{
				this._DataType = value;
				this.OnPropertyChanged("DataType");
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000B9E4 File Offset: 0x00009BE4
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0000B9EC File Offset: 0x00009BEC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsExpression")]
		public bool IsExpression
		{
			get
			{
				return this._IsExpression;
			}
			set
			{
				this._IsExpression = value;
				this.OnPropertyChanged("IsExpression");
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000BA00 File Offset: 0x00009C00
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000BA08 File Offset: 0x00009C08
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsMultiValued")]
		public bool IsMultiValued
		{
			get
			{
				return this._IsMultiValued;
			}
			set
			{
				this._IsMultiValued = value;
				this.OnPropertyChanged("IsMultiValued");
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060005E7 RID: 1511 RVA: 0x0000BA1C File Offset: 0x00009C1C
		// (remove) Token: 0x060005E8 RID: 1512 RVA: 0x0000BA54 File Offset: 0x00009C54
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000BA89 File Offset: 0x00009C89
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040002A1 RID: 673
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040002A2 RID: 674
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DefaultValue;

		// Token: 0x040002A3 RID: 675
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Nullable;

		// Token: 0x040002A4 RID: 676
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterType _DataType;

		// Token: 0x040002A5 RID: 677
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsExpression;

		// Token: 0x040002A6 RID: 678
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsMultiValued;
	}
}
