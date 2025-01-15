using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000115 RID: 277
	[OriginalName("DataSetParameterInfo")]
	public class DataSetParameterInfo : INotifyPropertyChanged
	{
		// Token: 0x06000BFC RID: 3068 RVA: 0x00017429 File Offset: 0x00015629
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

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0001744C File Offset: 0x0001564C
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00017454 File Offset: 0x00015654
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00017468 File Offset: 0x00015668
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x00017470 File Offset: 0x00015670
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00017484 File Offset: 0x00015684
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0001748C File Offset: 0x0001568C
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000174A0 File Offset: 0x000156A0
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x000174A8 File Offset: 0x000156A8
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

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x000174BC File Offset: 0x000156BC
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x000174C4 File Offset: 0x000156C4
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

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x000174D8 File Offset: 0x000156D8
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x000174E0 File Offset: 0x000156E0
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

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06000C09 RID: 3081 RVA: 0x000174F4 File Offset: 0x000156F4
		// (remove) Token: 0x06000C0A RID: 3082 RVA: 0x0001752C File Offset: 0x0001572C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000C0B RID: 3083 RVA: 0x00017561 File Offset: 0x00015761
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400056F RID: 1391
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000570 RID: 1392
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DefaultValue;

		// Token: 0x04000571 RID: 1393
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Nullable;

		// Token: 0x04000572 RID: 1394
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterType _DataType;

		// Token: 0x04000573 RID: 1395
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsExpression;

		// Token: 0x04000574 RID: 1396
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsMultiValued;
	}
}
