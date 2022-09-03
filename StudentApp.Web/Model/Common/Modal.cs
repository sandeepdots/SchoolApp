

using SchoolApp.Web.Code.Serialization;


namespace SchoolApp.Web.Models
{
    public class Modal
    {
        public string ID { get; set; }
        public string AreaLabeledId { get; set; }
        public ModalSize Size { get; set; }
        public string Message { get; set; }
        public string ModalSizeClass
        {
            get
            {
                switch (this.Size)
                {
                    case ModalSize.Small:
                        return "modal-sm";
                    case ModalSize.Large:
                        return " modal-lg";
                    case ModalSize.Medium:
                    default:
                        return "";
                }
            }
        }
        public bool IsHeader { get; set; }

        public ModalHeader Header { get; set; }
        public ModalFooter Footer { get; set; }

        public ModalFooter logoutID { get; set; }
    }
}