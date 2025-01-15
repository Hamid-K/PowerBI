using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200005A RID: 90
	internal class DeserializationContext
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0000C2D6 File Offset: 0x0000A4D6
		public DeserializationContext(SemanticModel model)
			: this(model, null)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		public DeserializationContext(SemanticModel model, ValidationContext validationCtx)
		{
			this.m_model = model;
			this.m_validationCtx = validationCtx ?? new ValidationContext();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000C30A File Offset: 0x0000A50A
		public ValidationContext Validation
		{
			get
			{
				return this.m_validationCtx;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000C312 File Offset: 0x0000A512
		internal SemanticModel CurrentModel
		{
			get
			{
				return this.m_model;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000C31A File Offset: 0x0000A51A
		internal SemanticQuery CurrentQuery
		{
			get
			{
				if (this.m_validationCtx.CurrentQuery != null && this.m_validationCtx.CurrentQuery.Model != this.m_model)
				{
					throw new InternalModelingException("CurrentQuery.Model does not match Model");
				}
				return this.m_validationCtx.CurrentQuery;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000C358 File Offset: 0x0000A558
		public void CompleteLoad()
		{
			this.m_completing = true;
			foreach (KeyValuePair<IDeserializationCallback, DeserializationContext.StateContainer> keyValuePair in this.m_stateContainers)
			{
				IDeserializationCallback key = keyValuePair.Key;
				DeserializationContext.StateContainer value = keyValuePair.Value;
				this.m_validationCtx.PushState(value.ValidationState);
				try
				{
					this.ProcessDeserializationReferences(key, value.ReferenceList);
				}
				finally
				{
					this.m_validationCtx.PopState();
				}
			}
			this.m_stateContainers.Clear();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000C400 File Offset: 0x0000A600
		internal void AddReference(IDeserializationCallback item, ModelingReference reference)
		{
			if (item == null)
			{
				throw new InternalModelingException("item is null");
			}
			if (this.m_completing)
			{
				throw new InternalModelingException("Cannot add reference during CompleteLoad");
			}
			DeserializationContext.StateContainer stateContainer;
			if (!this.m_stateContainers.TryGetValue(item, out stateContainer))
			{
				stateContainer = new DeserializationContext.StateContainer(this.m_validationCtx.GetCurrentState());
				this.m_stateContainers.Add(item, stateContainer);
			}
			stateContainer.ReferenceList.Add(reference);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000C46C File Offset: 0x0000A66C
		internal bool HasAnyReferences(IDeserializationCallback item)
		{
			DeserializationContext.StateContainer stateContainer;
			return this.m_stateContainers.TryGetValue(item, out stateContainer) && stateContainer.ReferenceList.Count > 0;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000C49C File Offset: 0x0000A69C
		internal bool HasReference(IDeserializationCallback item, string propertyName)
		{
			DeserializationContext.StateContainer stateContainer;
			return this.m_stateContainers.TryGetValue(item, out stateContainer) && stateContainer.ReferenceList.Exists((ModelingReference reference) => reference.PropertyName == propertyName);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		private void ProcessDeserializationReferences(IDeserializationCallback item, List<ModelingReference> refList)
		{
			foreach (ModelingReference modelingReference in refList)
			{
				try
				{
					if (!item.ProcessDeserializationReference(modelingReference, this))
					{
						string text = "Unhandled reference '";
						ModelingReference modelingReference2 = modelingReference;
						throw new InternalModelingException(text + modelingReference2.ToString() + "'");
					}
				}
				catch (ValidationException ex)
				{
					this.m_validationCtx.AddMessages(ex.Messages);
				}
			}
		}

		// Token: 0x0400021A RID: 538
		private readonly DeserializationContext.StateDictionary m_stateContainers = new DeserializationContext.StateDictionary();

		// Token: 0x0400021B RID: 539
		private readonly ValidationContext m_validationCtx;

		// Token: 0x0400021C RID: 540
		private readonly SemanticModel m_model;

		// Token: 0x0400021D RID: 541
		private bool m_completing;

		// Token: 0x02000128 RID: 296
		private class StateDictionary : Dictionary<IDeserializationCallback, DeserializationContext.StateContainer>
		{
			// Token: 0x06000DD4 RID: 3540 RVA: 0x0002D2C4 File Offset: 0x0002B4C4
			internal StateDictionary()
				: base(ObjectReferenceComparer<IDeserializationCallback>.Instance)
			{
			}
		}

		// Token: 0x02000129 RID: 297
		private struct StateContainer
		{
			// Token: 0x06000DD5 RID: 3541 RVA: 0x0002D2D1 File Offset: 0x0002B4D1
			public StateContainer(ValidationContext.State validationState)
			{
				this.ValidationState = validationState;
				this.ReferenceList = new List<ModelingReference>();
			}

			// Token: 0x040005BF RID: 1471
			public readonly ValidationContext.State ValidationState;

			// Token: 0x040005C0 RID: 1472
			public readonly List<ModelingReference> ReferenceList;
		}
	}
}
