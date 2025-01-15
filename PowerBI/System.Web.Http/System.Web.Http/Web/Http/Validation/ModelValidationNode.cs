using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;

namespace System.Web.Http.Validation
{
	// Token: 0x02000093 RID: 147
	public sealed class ModelValidationNode
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000A6BB File Offset: 0x000088BB
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey)
			: this(modelMetadata, modelStateKey, null)
		{
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000A6C8 File Offset: 0x000088C8
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey, IEnumerable<ModelValidationNode> childNodes)
		{
			if (modelMetadata == null)
			{
				throw Error.ArgumentNull("modelMetadata");
			}
			if (modelStateKey == null)
			{
				throw Error.ArgumentNull("modelStateKey");
			}
			this.ModelMetadata = modelMetadata;
			this.ModelStateKey = modelStateKey;
			this._childNodes = ((childNodes != null) ? childNodes.ToList<ModelValidationNode>() : new List<ModelValidationNode>());
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000390 RID: 912 RVA: 0x0000A71C File Offset: 0x0000891C
		// (remove) Token: 0x06000391 RID: 913 RVA: 0x0000A754 File Offset: 0x00008954
		public event EventHandler<ModelValidatedEventArgs> Validated;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000392 RID: 914 RVA: 0x0000A78C File Offset: 0x0000898C
		// (remove) Token: 0x06000393 RID: 915 RVA: 0x0000A7C4 File Offset: 0x000089C4
		public event EventHandler<ModelValidatingEventArgs> Validating;

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000A7F9 File Offset: 0x000089F9
		public ICollection<ModelValidationNode> ChildNodes
		{
			get
			{
				return this._childNodes;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000A801 File Offset: 0x00008A01
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000A809 File Offset: 0x00008A09
		public ModelMetadata ModelMetadata { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000A812 File Offset: 0x00008A12
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000A81A File Offset: 0x00008A1A
		public string ModelStateKey { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000A823 File Offset: 0x00008A23
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000A82B File Offset: 0x00008A2B
		public bool ValidateAllProperties { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000A834 File Offset: 0x00008A34
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000A83C File Offset: 0x00008A3C
		public bool SuppressValidation { get; set; }

		// Token: 0x0600039D RID: 925 RVA: 0x0000A848 File Offset: 0x00008A48
		public void CombineWith(ModelValidationNode otherNode)
		{
			if (otherNode != null && !otherNode.SuppressValidation)
			{
				this.Validated += otherNode.Validated;
				this.Validating += otherNode.Validating;
				List<ModelValidationNode> childNodes = otherNode._childNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					ModelValidationNode modelValidationNode = childNodes[i];
					this._childNodes.Add(modelValidationNode);
				}
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		private void OnValidated(ModelValidatedEventArgs e)
		{
			EventHandler<ModelValidatedEventArgs> validated = this.Validated;
			if (validated != null)
			{
				validated(this, e);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		private void OnValidating(ModelValidatingEventArgs e)
		{
			EventHandler<ModelValidatingEventArgs> validating = this.Validating;
			if (validating != null)
			{
				validating(this, e);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		private object TryConvertContainerToMetadataType(ModelValidationNode parentNode)
		{
			if (parentNode != null)
			{
				object model = parentNode.ModelMetadata.Model;
				if (model != null)
				{
					Type containerType = this.ModelMetadata.ContainerType;
					if (containerType != null && containerType.IsInstanceOfType(model))
					{
						return model;
					}
				}
			}
			return null;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000A924 File Offset: 0x00008B24
		public void Validate(HttpActionContext actionContext)
		{
			this.Validate(actionContext, null);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000A930 File Offset: 0x00008B30
		public void Validate(HttpActionContext actionContext, ModelValidationNode parentNode)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (this.SuppressValidation)
			{
				return;
			}
			ModelValidatingEventArgs modelValidatingEventArgs = new ModelValidatingEventArgs(actionContext, parentNode);
			this.OnValidating(modelValidatingEventArgs);
			if (modelValidatingEventArgs.Cancel)
			{
				return;
			}
			this.ValidateChildren(actionContext);
			this.ValidateThis(actionContext, parentNode);
			ModelValidatedEventArgs modelValidatedEventArgs = new ModelValidatedEventArgs(actionContext, parentNode);
			this.OnValidated(modelValidatedEventArgs);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000A98C File Offset: 0x00008B8C
		private void ValidateChildren(HttpActionContext actionContext)
		{
			for (int i = 0; i < this._childNodes.Count; i++)
			{
				this._childNodes[i].Validate(actionContext, this);
			}
			if (this.ValidateAllProperties)
			{
				this.ValidateProperties(actionContext);
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000A9D4 File Offset: 0x00008BD4
		private void ValidateProperties(HttpActionContext actionContext)
		{
			ModelStateDictionary modelState = actionContext.ModelState;
			object model = this.ModelMetadata.Model;
			foreach (ModelMetadata modelMetadata in actionContext.GetMetadataProvider().GetMetadataForType(() => model, this.ModelMetadata.ModelType).Properties)
			{
				string text = ModelBindingHelper.CreatePropertyModelName(this.ModelStateKey, modelMetadata.PropertyName);
				if (modelState.IsValidField(text))
				{
					foreach (ModelValidator modelValidator in actionContext.GetValidators(modelMetadata))
					{
						foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(modelMetadata, model))
						{
							string text2 = ModelBindingHelper.CreatePropertyModelName(text, modelValidationResult.MemberName);
							modelState.AddModelError(text2, modelValidationResult.Message);
						}
					}
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000AB1C File Offset: 0x00008D1C
		private void ValidateThis(HttpActionContext actionContext, ModelValidationNode parentNode)
		{
			ModelStateDictionary modelState = actionContext.ModelState;
			if (!modelState.IsValidField(this.ModelStateKey))
			{
				return;
			}
			if (parentNode == null && this.ModelMetadata.Model == null)
			{
				string text = ModelBindingHelper.CreatePropertyModelName(this.ModelStateKey, this.ModelMetadata.GetDisplayName());
				modelState.AddModelError(text, SRResources.Validation_ValueNotFound);
				return;
			}
			this._validators = actionContext.GetValidators(this.ModelMetadata);
			object obj = this.TryConvertContainerToMetadataType(parentNode);
			ModelValidator[] array = this._validators.AsArray<ModelValidator>();
			for (int i = 0; i < array.Length; i++)
			{
				foreach (ModelValidationResult modelValidationResult in array[i].Validate(this.ModelMetadata, obj))
				{
					string text2 = ModelBindingHelper.CreatePropertyModelName(this.ModelStateKey, modelValidationResult.MemberName);
					modelState.AddModelError(text2, modelValidationResult.Message);
				}
			}
		}

		// Token: 0x040000CF RID: 207
		private IEnumerable<ModelValidator> _validators;

		// Token: 0x040000D0 RID: 208
		private readonly List<ModelValidationNode> _childNodes;
	}
}
