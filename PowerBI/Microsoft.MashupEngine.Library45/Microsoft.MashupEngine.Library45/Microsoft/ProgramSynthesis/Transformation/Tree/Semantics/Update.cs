using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics
{
	// Token: 0x02001ECB RID: 7883
	[DataContract(Namespace = "Microsoft.ProgramSynthesis.Transformation.Tree.Semantics")]
	public class Update : Edit
	{
		// Token: 0x06010A23 RID: 68131 RVA: 0x0039464D File Offset: 0x0039284D
		public Update(Node newNode, Node target)
			: base(target, newNode)
		{
		}

		// Token: 0x06010A24 RID: 68132 RVA: 0x00394658 File Offset: 0x00392858
		public override XElement SerializeToXml()
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Update));
			return base.SerializeHelper(dataContractSerializer);
		}

		// Token: 0x06010A25 RID: 68133 RVA: 0x0039467C File Offset: 0x0039287C
		public static Update DeserializeFromXml(XElement update)
		{
			if (update == null)
			{
				throw new ArgumentException("update cannot be null.", "update");
			}
			return XmlUtils.DeserializeFromXml<Update>(update);
		}
	}
}
