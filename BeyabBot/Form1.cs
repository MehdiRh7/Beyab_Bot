using DataLayer;
using DataLayer.HandlersServices;
using DataLayer.Services;
using DataLayer.Utilities;
using System;
using System.Windows.Forms;

namespace BeyabBot
{
    public partial class Form1 : Form
    {
        private readonly BeyabContext _db;
        private readonly IOnlinesRepository _onlinesRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IBlockRepository _blockRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly IValidationRepository _validationRepository;

        private BotRunner _botRunner;
        private KeyboardProvider _keyboardProvider;

        // Put your token here (or better: move to config)
        private static readonly string Token = "5597459849:AAFGGWu-bS6NKDiHGLQas8D64_UYKzvYWv0";

        public Form1()
        {
            InitializeComponent();

            _db = new BeyabContext();
            _onlinesRepository = new OnlinesRepository(_db);
            _personRepository = new PersonRepository(_db);
            _blockRepository = new BlockRepository(_db);
            _friendRepository = new FriendRepository(_db);
            _validationRepository = new ValidationRepository(_db);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _keyboardProvider = new KeyboardProvider();

            var messageHandler = new MessageHandler(_personRepository, _onlinesRepository, _validationRepository, _keyboardProvider);
            var callbackHandler = new CallbackHandler(_personRepository, _friendRepository, _onlinesRepository, _blockRepository, _validationRepository, _keyboardProvider);

            _botRunner = new BotRunner(Token, messageHandler, callbackHandler);
            _botRunner.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _botRunner?.Stop();
            _botRunner?.Dispose();
        }
    }
}