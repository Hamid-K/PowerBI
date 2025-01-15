using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002149 RID: 8521
	[Serializable]
	internal sealed class PartExtensionProvider : Dictionary<string, string>
	{
		// Token: 0x0600D3D1 RID: 54225 RVA: 0x0027A051 File Offset: 0x00278251
		public PartExtensionProvider()
		{
		}

		// Token: 0x0600D3D2 RID: 54226 RVA: 0x002A1594 File Offset: 0x0029F794
		public PartExtensionProvider(PartExtensionProvider partExtProvider)
			: base(partExtProvider)
		{
		}

		// Token: 0x0600D3D3 RID: 54227 RVA: 0x0027A059 File Offset: 0x00278259
		public PartExtensionProvider(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x0600D3D4 RID: 54228 RVA: 0x002A159D File Offset: 0x0029F79D
		private PartExtensionProvider(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600D3D5 RID: 54229 RVA: 0x002A15A7 File Offset: 0x0029F7A7
		public void AddPartExtension(string contentType, string partExtension)
		{
			base.Add(contentType, partExtension);
		}

		// Token: 0x0600D3D6 RID: 54230 RVA: 0x002A15B4 File Offset: 0x0029F7B4
		public void MakeSurePartExtensionExist(string contentType, string partExtension)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (partExtension == null)
			{
				throw new ArgumentNullException("partExtension");
			}
			string text = null;
			if (base.TryGetValue(contentType, out text) && text == partExtension)
			{
				return;
			}
			base.Add(contentType, partExtension);
		}
	}
}
