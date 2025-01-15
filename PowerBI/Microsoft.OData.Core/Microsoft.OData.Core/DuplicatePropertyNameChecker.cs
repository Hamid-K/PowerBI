using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x02000020 RID: 32
	internal class DuplicatePropertyNameChecker : IDuplicatePropertyNameChecker
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public void ValidatePropertyUniqueness(ODataPropertyInfo property)
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

		// Token: 0x06000155 RID: 341 RVA: 0x00003D28 File Offset: 0x00001F28
		public void ValidatePropertyUniqueness(ODataNestedResourceInfo property)
		{
			DuplicatePropertyNameChecker.State state;
			if (!this.propertyState.TryGetValue(property.Name, out state))
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

		// Token: 0x06000156 RID: 342 RVA: 0x00003D84 File Offset: 0x00001F84
		public void ValidatePropertyOpenForAssociationLink(string propertyName)
		{
			DuplicatePropertyNameChecker.State state;
			if (!this.propertyState.TryGetValue(propertyName, out state))
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

		// Token: 0x06000157 RID: 343 RVA: 0x00003DCC File Offset: 0x00001FCC
		public void Reset()
		{
			this.propertyState.Clear();
		}

		// Token: 0x04000062 RID: 98
		private IDictionary<string, DuplicatePropertyNameChecker.State> propertyState = new Dictionary<string, DuplicatePropertyNameChecker.State>();

		// Token: 0x0200027E RID: 638
		[Flags]
		private enum State
		{
			// Token: 0x04000BD2 RID: 3026
			NonNestedResource = 0,
			// Token: 0x04000BD3 RID: 3027
			NestedResource = 1,
			// Token: 0x04000BD4 RID: 3028
			AssociationLink = 2
		}
	}
}
