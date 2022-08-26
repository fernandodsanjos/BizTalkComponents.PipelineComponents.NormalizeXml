using BizTalkComponents.Utils;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Streaming;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;

namespace BizTalkComponents.PipelineComponents.NormalizeXml
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("38391ba2-b165-4a59-a18b-45ae2dc8f625")]
    [ComponentCategory(CategoryTypes.CATID_Decoder)]
    public partial class NormalizeXml : IComponent, IBaseComponent, IComponentUI
    {
        #region Name & Description
        //error is added so one does not forget to change Name and Description

        public string Name
        {
            get
            {
                return "Normalize XML";

            }
        }

        public string Version { get { return "1.0"; } }


        public string Description
        {
            get
            {
                return "Normalizes xml output, primarily from WCF-SQL adapter";

            }
        }
        #endregion

        #region Properties
        [Description("Disable component")]
        [RequiredRuntime]
        public bool Disable { get; set; } = false;

        [Description("Adds namespace to root node and removes it from subsequent nodes. Prefix cn is used on subsequent nodes instead of namespace")]
        public string Namespace { get; set; }
        #endregion
        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            if (Disable)
                return pInMsg;

            if(pInMsg?.BodyPart?.Data == null)
                return pInMsg;

            NormalizeXmlStream nor = new NormalizeXmlStream(XmlReader.Create(pInMsg.BodyPart.Data));

            pContext.ResourceTracker.AddResource(nor);

            pInMsg.BodyPart.Data = nor;


            return pInMsg;
        }


    }
}
