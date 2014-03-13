using System;
using System.ComponentModel.DataAnnotations;

namespace RebusWebExample.Messaging
{
    public class ChangeTaskNameCommand
        : ICommand
    {
        [Required]
        public Guid? TaskId { get; set; }
        [Required]
        public string NewName { get; set; }
    }
}