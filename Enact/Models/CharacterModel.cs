using Enact.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enact.Models
{
    public class CharacterModel : INotified
    {
        public enum CharacterType
        {
            Controller,
            Duelist,
            Sentinel,
            Initiator
        }

        private string? _name;
        public string? Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private Uri? _splash;
        public Uri? Splash
        {
            get { return _splash; }
            set { SetProperty(ref _splash, value); }
        }

        private Uri? _background;
        public Uri? Background
        {
            get { return _background; }
            set { SetProperty(ref _background, value); }
        }

        private CharacterType _type;
        public CharacterType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        public CharacterModel(string name, CharacterType type, Uri splash, Uri background)
        {
            Name = name;
            Type = type;
            Splash = splash;
            Background = background;
        }
    }
}
