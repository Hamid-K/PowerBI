using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E0 RID: 224
	public class EdmDirectValueAnnotationsManager : IEdmDirectValueAnnotationsManager
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x000116F8 File Offset: 0x0000F8F8
		public EdmDirectValueAnnotationsManager()
		{
			this.annotationsDictionary = VersioningDictionary<IEdmElement, object>.Create(new Func<IEdmElement, IEdmElement, int>(this.CompareElements));
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00011738 File Offset: 0x0000F938
		public IEnumerable<IEdmDirectValueAnnotation> GetDirectValueAnnotations(IEdmElement element)
		{
			VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
			IEnumerable<IEdmDirectValueAnnotation> attachedAnnotations = this.GetAttachedAnnotations(element);
			object transientAnnotations = EdmDirectValueAnnotationsManager.GetTransientAnnotations(element, versioningDictionary);
			IEnumerator<IEdmDirectValueAnnotation> enumerator;
			if (attachedAnnotations != null)
			{
				foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in attachedAnnotations)
				{
					if (!EdmDirectValueAnnotationsManager.IsDead(edmDirectValueAnnotation.NamespaceUri, edmDirectValueAnnotation.Name, transientAnnotations))
					{
						yield return edmDirectValueAnnotation;
					}
				}
				enumerator = null;
			}
			foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation2 in EdmDirectValueAnnotationsManager.TransientAnnotations(transientAnnotations))
			{
				yield return edmDirectValueAnnotation2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00011750 File Offset: 0x0000F950
		public void SetAnnotationValue(IEdmElement element, string namespaceName, string localName, object value)
		{
			object obj = this.annotationsDictionaryLock;
			lock (obj)
			{
				VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
				this.SetAnnotationValue(element, namespaceName, localName, value, ref versioningDictionary);
				this.annotationsDictionary = versioningDictionary;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000117A0 File Offset: 0x0000F9A0
		public void SetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			object obj = this.annotationsDictionaryLock;
			lock (obj)
			{
				VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
				foreach (IEdmDirectValueAnnotationBinding edmDirectValueAnnotationBinding in annotations)
				{
					this.SetAnnotationValue(edmDirectValueAnnotationBinding.Element, edmDirectValueAnnotationBinding.NamespaceUri, edmDirectValueAnnotationBinding.Name, edmDirectValueAnnotationBinding.Value, ref versioningDictionary);
				}
				this.annotationsDictionary = versioningDictionary;
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00011834 File Offset: 0x0000FA34
		public object GetAnnotationValue(IEdmElement element, string namespaceName, string localName)
		{
			VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
			return this.GetAnnotationValue(element, namespaceName, localName, versioningDictionary);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00011854 File Offset: 0x0000FA54
		public object[] GetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			VersioningDictionary<IEdmElement, object> versioningDictionary = this.annotationsDictionary;
			object[] array = new object[Enumerable.Count<IEdmDirectValueAnnotationBinding>(annotations)];
			int num = 0;
			foreach (IEdmDirectValueAnnotationBinding edmDirectValueAnnotationBinding in annotations)
			{
				array[num++] = this.GetAnnotationValue(edmDirectValueAnnotationBinding.Element, edmDirectValueAnnotationBinding.NamespaceUri, edmDirectValueAnnotationBinding.Name, versioningDictionary);
			}
			return array;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00008D69 File Offset: 0x00006F69
		protected virtual IEnumerable<IEdmDirectValueAnnotation> GetAttachedAnnotations(IEdmElement element)
		{
			return null;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000118D0 File Offset: 0x0000FAD0
		private static void SetAnnotation(IEnumerable<IEdmDirectValueAnnotation> immutableAnnotations, ref object transientAnnotations, string namespaceName, string localName, object value)
		{
			bool flag = false;
			if (immutableAnnotations != null && Enumerable.Any<IEdmDirectValueAnnotation>(immutableAnnotations, (IEdmDirectValueAnnotation existingAnnotation) => existingAnnotation.NamespaceUri == namespaceName && existingAnnotation.Name == localName))
			{
				flag = true;
			}
			if (value == null && !flag)
			{
				EdmDirectValueAnnotationsManager.RemoveTransientAnnotation(ref transientAnnotations, namespaceName, localName);
				return;
			}
			if (namespaceName == "http://schemas.microsoft.com/ado/2011/04/edm/documentation" && value != null && !(value is IEdmDocumentation))
			{
				throw new InvalidOperationException(Strings.Annotations_DocumentationPun(value.GetType().Name));
			}
			IEdmDirectValueAnnotation edmDirectValueAnnotation = ((value != null) ? new EdmDirectValueAnnotation(namespaceName, localName, value) : new EdmDirectValueAnnotation(namespaceName, localName));
			if (transientAnnotations == null)
			{
				transientAnnotations = edmDirectValueAnnotation;
				return;
			}
			IEdmDirectValueAnnotation edmDirectValueAnnotation2 = transientAnnotations as IEdmDirectValueAnnotation;
			if (edmDirectValueAnnotation2 == null)
			{
				VersioningList<IEdmDirectValueAnnotation> versioningList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
				for (int i = 0; i < versioningList.Count; i++)
				{
					IEdmDirectValueAnnotation edmDirectValueAnnotation3 = versioningList[i];
					if (edmDirectValueAnnotation3.NamespaceUri == namespaceName && edmDirectValueAnnotation3.Name == localName)
					{
						versioningList = versioningList.RemoveAt(i);
						break;
					}
				}
				transientAnnotations = versioningList.Add(edmDirectValueAnnotation);
				return;
			}
			if (edmDirectValueAnnotation2.NamespaceUri == namespaceName && edmDirectValueAnnotation2.Name == localName)
			{
				transientAnnotations = edmDirectValueAnnotation;
				return;
			}
			transientAnnotations = VersioningList<IEdmDirectValueAnnotation>.Create().Add(edmDirectValueAnnotation2).Add(edmDirectValueAnnotation);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00011A44 File Offset: 0x0000FC44
		private static IEdmDirectValueAnnotation FindTransientAnnotation(object transientAnnotations, string namespaceName, string localName)
		{
			if (transientAnnotations != null)
			{
				IEdmDirectValueAnnotation edmDirectValueAnnotation = transientAnnotations as IEdmDirectValueAnnotation;
				if (edmDirectValueAnnotation == null)
				{
					VersioningList<IEdmDirectValueAnnotation> versioningList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
					return Enumerable.FirstOrDefault<IEdmDirectValueAnnotation>(versioningList, (IEdmDirectValueAnnotation existingAnnotation) => existingAnnotation.NamespaceUri == namespaceName && existingAnnotation.Name == localName);
				}
				if (edmDirectValueAnnotation.NamespaceUri == namespaceName && edmDirectValueAnnotation.Name == localName)
				{
					return edmDirectValueAnnotation;
				}
			}
			return null;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		private static void RemoveTransientAnnotation(ref object transientAnnotations, string namespaceName, string localName)
		{
			if (transientAnnotations != null)
			{
				IEdmDirectValueAnnotation edmDirectValueAnnotation = transientAnnotations as IEdmDirectValueAnnotation;
				if (edmDirectValueAnnotation != null)
				{
					if (edmDirectValueAnnotation.NamespaceUri == namespaceName && edmDirectValueAnnotation.Name == localName)
					{
						transientAnnotations = null;
						return;
					}
				}
				else
				{
					VersioningList<IEdmDirectValueAnnotation> versioningList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
					int i = 0;
					while (i < versioningList.Count)
					{
						IEdmDirectValueAnnotation edmDirectValueAnnotation2 = versioningList[i];
						if (edmDirectValueAnnotation2.NamespaceUri == namespaceName && edmDirectValueAnnotation2.Name == localName)
						{
							versioningList = versioningList.RemoveAt(i);
							if (versioningList.Count == 1)
							{
								transientAnnotations = Enumerable.Single<IEdmDirectValueAnnotation>(versioningList);
								return;
							}
							transientAnnotations = versioningList;
							return;
						}
						else
						{
							i++;
						}
					}
				}
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00011B52 File Offset: 0x0000FD52
		private static IEnumerable<IEdmDirectValueAnnotation> TransientAnnotations(object transientAnnotations)
		{
			if (transientAnnotations == null)
			{
				yield break;
			}
			IEdmDirectValueAnnotation edmDirectValueAnnotation = transientAnnotations as IEdmDirectValueAnnotation;
			if (edmDirectValueAnnotation != null)
			{
				if (edmDirectValueAnnotation.Value != null)
				{
					yield return edmDirectValueAnnotation;
				}
				yield break;
			}
			VersioningList<IEdmDirectValueAnnotation> versioningList = (VersioningList<IEdmDirectValueAnnotation>)transientAnnotations;
			foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation2 in versioningList)
			{
				if (edmDirectValueAnnotation2.Value != null)
				{
					yield return edmDirectValueAnnotation2;
				}
			}
			IEnumerator<IEdmDirectValueAnnotation> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00011B62 File Offset: 0x0000FD62
		private static bool IsDead(string namespaceName, string localName, object transientAnnotations)
		{
			return EdmDirectValueAnnotationsManager.FindTransientAnnotation(transientAnnotations, namespaceName, localName) != null;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00011B70 File Offset: 0x0000FD70
		private static object GetTransientAnnotations(IEdmElement element, VersioningDictionary<IEdmElement, object> annotationsDictionary)
		{
			object obj;
			annotationsDictionary.TryGetValue(element, out obj);
			return obj;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00011B88 File Offset: 0x0000FD88
		private void SetAnnotationValue(IEdmElement element, string namespaceName, string localName, object value, ref VersioningDictionary<IEdmElement, object> annotationsDictionary)
		{
			object transientAnnotations = EdmDirectValueAnnotationsManager.GetTransientAnnotations(element, annotationsDictionary);
			object obj = transientAnnotations;
			EdmDirectValueAnnotationsManager.SetAnnotation(this.GetAttachedAnnotations(element), ref transientAnnotations, namespaceName, localName, value);
			if (transientAnnotations != obj)
			{
				annotationsDictionary = annotationsDictionary.Set(element, transientAnnotations);
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		private object GetAnnotationValue(IEdmElement element, string namespaceName, string localName, VersioningDictionary<IEdmElement, object> annotationsDictionary)
		{
			IEdmDirectValueAnnotation edmDirectValueAnnotation = EdmDirectValueAnnotationsManager.FindTransientAnnotation(EdmDirectValueAnnotationsManager.GetTransientAnnotations(element, annotationsDictionary), namespaceName, localName);
			if (edmDirectValueAnnotation != null)
			{
				return edmDirectValueAnnotation.Value;
			}
			IEnumerable<IEdmDirectValueAnnotation> attachedAnnotations = this.GetAttachedAnnotations(element);
			if (attachedAnnotations != null)
			{
				foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation2 in attachedAnnotations)
				{
					if (edmDirectValueAnnotation2.NamespaceUri == namespaceName && edmDirectValueAnnotation2.Name == localName)
					{
						return edmDirectValueAnnotation2.Value;
					}
				}
			}
			return null;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00011C54 File Offset: 0x0000FE54
		private int CompareElements(IEdmElement left, IEdmElement right)
		{
			if (left == right)
			{
				return 0;
			}
			int hashCode = left.GetHashCode();
			int hashCode2 = right.GetHashCode();
			if (hashCode < hashCode2)
			{
				return -1;
			}
			if (hashCode > hashCode2)
			{
				return 1;
			}
			IEdmNamedElement edmNamedElement = left as IEdmNamedElement;
			IEdmNamedElement edmNamedElement2 = right as IEdmNamedElement;
			if (edmNamedElement == null)
			{
				if (edmNamedElement2 != null)
				{
					return -1;
				}
			}
			else
			{
				if (edmNamedElement2 == null)
				{
					return 1;
				}
				int num = string.Compare(edmNamedElement.Name, edmNamedElement2.Name, 4);
				if (num != 0)
				{
					return num;
				}
			}
			for (;;)
			{
				foreach (IEdmElement edmElement in this.unsortedElements)
				{
					if (edmElement == left)
					{
						return 1;
					}
					if (edmElement == right)
					{
						return -1;
					}
				}
				object obj = this.unsortedElementsLock;
				lock (obj)
				{
					this.unsortedElements = this.unsortedElements.Add(left);
					continue;
				}
				break;
			}
			int num2;
			return num2;
		}

		// Token: 0x040003ED RID: 1005
		private VersioningDictionary<IEdmElement, object> annotationsDictionary;

		// Token: 0x040003EE RID: 1006
		private object annotationsDictionaryLock = new object();

		// Token: 0x040003EF RID: 1007
		private VersioningList<IEdmElement> unsortedElements = VersioningList<IEdmElement>.Create();

		// Token: 0x040003F0 RID: 1008
		private object unsortedElementsLock = new object();
	}
}
