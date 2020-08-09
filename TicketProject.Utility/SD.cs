using System;
using System.Collections.Generic;
using System.Text;

namespace TicketProject.Utility
{
    public static class SD
    {
        public const string Role_User = "User";
        public const string Role_CompanyAdmin = "Company Admin";
        public const string Role_Admin = "Application Admin";
        public const string Role_CompanyAgents = "Company Agent";
        public const string Role_SupportUsers = "Support Users";
        //Ticket Management değikenleri...
        public const string Ticket_Create = "ticket was created";
        public const string Ticket_Agent = "send to the agent";
        public const string Ticket_SupportUser = "send to the support user";
        public const string Ticket_Finish = "problem solved";
    }
}
