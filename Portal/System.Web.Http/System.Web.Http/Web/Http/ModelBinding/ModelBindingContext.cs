using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;
using System.Web.Http.Validation;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200005E RID: 94
	public class ModelBindingContext
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00007BBD File Offset: 0x00005DBD
		public ModelBindingContext()
			: this(null)
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007BC6 File Offset: 0x00005DC6
		public ModelBindingContext(ModelBindingContext bindingContext)
		{
			if (bindingContext != null)
			{
				this.ModelState = bindingContext.ModelState;
				this.ValueProvider = bindingContext.ValueProvider;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00007BE9 File Offset: 0x00005DE9
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00007BFC File Offset: 0x00005DFC
		public object Model
		{
			get
			{
				this.EnsureModelMetadata();
				return this.ModelMetadata.Model;
			}
			set
			{
				this.EnsureModelMetadata();
				this.ModelMetadata.Model = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00007C10 File Offset: 0x00005E10
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00007C18 File Offset: 0x00005E18
		public ModelMetadata ModelMetadata { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00007C21 File Offset: 0x00005E21
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00007C3C File Offset: 0x00005E3C
		public string ModelName
		{
			get
			{
				if (this._modelName == null)
				{
					this._modelName = string.Empty;
				}
				return this._modelName;
			}
			set
			{
				this._modelName = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00007C45 File Offset: 0x00005E45
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00007C60 File Offset: 0x00005E60
		public ModelStateDictionary ModelState
		{
			get
			{
				if (this._modelState == null)
				{
					this._modelState = new ModelStateDictionary();
				}
				return this._modelState;
			}
			set
			{
				this._modelState = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00007C69 File Offset: 0x00005E69
		public Type ModelType
		{
			get
			{
				this.EnsureModelMetadata();
				return this.ModelMetadata.ModelType;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00007C7C File Offset: 0x00005E7C
		public IDictionary<string, ModelMetadata> PropertyMetadata
		{
			get
			{
				if (this._propertyMetadata == null)
				{
					this._propertyMetadata = this.ModelMetadata.Properties.ToDictionary((ModelMetadata m) => m.PropertyName, StringComparer.OrdinalIgnoreCase);
				}
				return this._propertyMetadata;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007CD1 File Offset: 0x00005ED1
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00007CF8 File Offset: 0x00005EF8
		public ModelValidationNode ValidationNode
		{
			get
			{
				if (this._validationNode == null)
				{
					this._validationNode = new ModelValidationNode(this.ModelMetadata, this.ModelName);
				}
				return this._validationNode;
			}
			set
			{
				this._validationNode = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00007D01 File Offset: 0x00005F01
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00007D09 File Offset: 0x00005F09
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00007D12 File Offset: 0x00005F12
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00007D1A File Offset: 0x00005F1A
		public bool FallbackToEmptyPrefix { get; set; }

		// Token: 0x06000297 RID: 663 RVA: 0x00007D23 File Offset: 0x00005F23
		private void EnsureModelMetadata()
		{
			if (this.ModelMetadata == null)
			{
				throw Error.InvalidOperation(SRResources.ModelBindingContext_ModelMetadataMustBeSet, new object[0]);
			}
		}

		// Token: 0x04000091 RID: 145
		private string _modelName;

		// Token: 0x04000092 RID: 146
		private ModelStateDictionary _modelState;

		// Token: 0x04000093 RID: 147
		private Dictionary<string, ModelMetadata> _propertyMetadata;

		// Token: 0x04000094 RID: 148
		private ModelValidationNode _validationNode;
	}
}
