using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x020000BB RID: 187
	internal class DuplicatePropertyNameChecker : IDuplicatePropertyNameChecker
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x00015160 File Offset: 0x00013360
		public void ValidatePropertyUniqueness(ODataProperty property)
		{
			try
			{
				this.propertyState.Add(property.Name, DuplicatePropertyNameChecker.State.NonNestedResource);
			}
			catch (ArgumentException)
			{
				throw new ODataException(Strings.DuplicatePropertyNamesNotAllowed(property.Name));
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000151A4 File Offset: 0x000133A4
		public void ValidatePropertyUniqueness(ODataNestedResourceInfo property)
		{
			DuplicatePropertyNameChecker.State state;
			if (!this.propertyState.TryGetValue(property.Name, ref state))
			{
				this.propertyState[property.Name] = DuplicatePropertyNameChecker.State.NestedResource;
				return;
			}
			if (state != DuplicatePropertyNameChecker.State.AssociationLink)
			{
				throw new ODataException(Strings.DuplicatePropertyNamesNotAllowed(property.Name));
			}
			this.propertyState[property.Name] = DuplicatePropertyNameChecker.State.NestedResource | DuplicatePropertyNameChecker.State.AssociationLink;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00015200 File Offset: 0x00013400
		public void ValidatePropertyOpenForAssociationLink(string propertyName)
		{
			DuplicatePropertyNameChecker.State state;
			if (!this.propertyState.TryGetValue(propertyName, ref state))
			{
				this.propertyState[propertyName] = DuplicatePropertyNameChecker.State.AssociationLink;
				return;
			}
			if (state != DuplicatePropertyNameChecker.State.NestedResource)
			{
				throw new ODataException(Strings.DuplicatePropertyNamesNotAllowed(propertyName));
			}
			this.propertyState[propertyName] = DuplicatePropertyNameChecker.State.NestedResource | DuplicatePropertyNameChecker.State.AssociationLink;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00015248 File Offset: 0x00013448
		public void Reset()
		{
			this.propertyState.Clear();
		}

		// Token: 0x04000303 RID: 771
		private IDictionary<string, DuplicatePropertyNameChecker.State> propertyState = new Dictionary<string, DuplicatePropertyNameChecker.State>();

		// Token: 0x0200029F RID: 671
		[Flags]
		private enum State
		{
			// Token: 0x04000BB0 RID: 2992
			NonNestedResource = 0,
			// Token: 0x04000BB1 RID: 2993
			NestedResource = 1,
			// Token: 0x04000BB2 RID: 2994
			AssociationLink = 2
		}
	}
}
