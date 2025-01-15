using System;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000106 RID: 262
	internal class InternalEntityPropertyEntry : InternalPropertyEntry
	{
		// Token: 0x060012C8 RID: 4808 RVA: 0x000316E4 File Offset: 0x0002F8E4
		public InternalEntityPropertyEntry(InternalEntityEntry internalEntityEntry, PropertyEntryMetadata propertyMetadata)
			: base(internalEntityEntry, propertyMetadata)
		{
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x000316EE File Offset: 0x0002F8EE
		public override InternalPropertyEntry ParentPropertyEntry
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x000316F1 File Offset: 0x0002F8F1
		public override InternalPropertyValues ParentCurrentValues
		{
			get
			{
				return this.InternalEntityEntry.CurrentValues;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x000316FE File Offset: 0x0002F8FE
		public override InternalPropertyValues ParentOriginalValues
		{
			get
			{
				return this.InternalEntityEntry.OriginalValues;
			}
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0003170C File Offset: 0x0002F90C
		protected override Func<object, object> CreateGetter()
		{
			Func<object, object> func;
			DbHelpers.GetPropertyGetters(this.InternalEntityEntry.EntityType).TryGetValue(this.Name, out func);
			return func;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00031738 File Offset: 0x0002F938
		protected override Action<object, object> CreateSetter()
		{
			Action<object, object> action;
			DbHelpers.GetPropertySetters(this.InternalEntityEntry.EntityType).TryGetValue(this.Name, out action);
			return action;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00031764 File Offset: 0x0002F964
		public override bool EntityPropertyIsModified()
		{
			return this.InternalEntityEntry.ObjectStateEntry.GetModifiedProperties().Contains(this.Name);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00031781 File Offset: 0x0002F981
		public override void SetEntityPropertyModified()
		{
			this.InternalEntityEntry.ObjectStateEntry.SetModifiedProperty(this.Name);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00031799 File Offset: 0x0002F999
		public override void RejectEntityPropertyChanges()
		{
			this.InternalEntityEntry.ObjectStateEntry.RejectPropertyChanges(this.Name);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000317B1 File Offset: 0x0002F9B1
		public override void UpdateComplexPropertyState()
		{
			if (!this.InternalEntityEntry.ObjectStateEntry.IsPropertyChanged(this.Name))
			{
				this.RejectEntityPropertyChanges();
			}
		}
	}
}
