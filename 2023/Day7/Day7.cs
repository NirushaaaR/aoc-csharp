using System.Text;

namespace AOC.Y2023;

class Day7(string inputName = "input.txt") : BaseDay(2023, 7, inputName)
{
	enum HandType
	{
		HIGH_CARD,
		ONE_PAIR,
		TWO_PAIR,
		THREE_OF_A_KIND,
		FULL_HOUSE,
		FOUR_OF_A_KIND,
		FIVE_OF_A_KIND
	}

	struct Hand
	{
		public HandType type;
		public string cards;
		public int bid;

		public Hand(string cards, int bid)
		{
			this.cards = cards;
			this.bid = bid;
			type = HandType.HIGH_CARD;

			Dictionary<char, int> sameLabel = [];
			foreach (var card in cards)
			{
				sameLabel.TryGetValue(card, out int value);
				sameLabel[card] = value + 1;
			}


			foreach (var keyValuePair in sameLabel)
			{
				if (keyValuePair.Value == 5)
				{
					type = HandType.FIVE_OF_A_KIND;
				}
				else if (keyValuePair.Value == 4)
				{
					type = HandType.FOUR_OF_A_KIND;
				}
				else if (keyValuePair.Value == 3)
				{
					if (type == HandType.ONE_PAIR)
					{
						type = HandType.FULL_HOUSE;
					}
					else
					{
						type = HandType.THREE_OF_A_KIND;
					}
				}
				else if (keyValuePair.Value == 2)
				{
					if (type == HandType.ONE_PAIR)
					{
						type = HandType.TWO_PAIR;
					}
					else if (type == HandType.THREE_OF_A_KIND)
					{
						type = HandType.FULL_HOUSE;
					}
					else
					{
						type = HandType.ONE_PAIR;
					}
				}
			}
		}
	}

	struct HandWithJoker
	{
		public Hand hand; // a hand without joker in it
		public string cards; // a real hand with joker in it
		private int jokerCount;

		public HandWithJoker(string cards, int bid)
		{
			this.cards = cards;
			jokerCount = 0;
			StringBuilder nonJokerCards = new();
			foreach (var card in cards)
			{
				if (card == 'J')
				{
					jokerCount++;
				}
				else
				{
					nonJokerCards.Append(card);
				}
			}

			hand = new(nonJokerCards.ToString(), bid);

			if (hand.type == HandType.FOUR_OF_A_KIND)
			{
				if (jokerCount == 1)
				{
					hand.type = HandType.FIVE_OF_A_KIND;
				}
			}
			else if (hand.type == HandType.THREE_OF_A_KIND)
			{
				if (jokerCount == 1)
				{
					hand.type = HandType.FOUR_OF_A_KIND;
				}
				else if (jokerCount == 2)
				{
					hand.type = HandType.FIVE_OF_A_KIND;
				}
			}
			else if (hand.type == HandType.TWO_PAIR)
			{
				if (jokerCount == 1)
				{
					hand.type = HandType.FULL_HOUSE;
				}
			}
			else if (hand.type == HandType.ONE_PAIR)
			{
				if (jokerCount == 1)
				{
					hand.type = HandType.THREE_OF_A_KIND;
				}
				else if (jokerCount == 2)
				{
					hand.type = HandType.FOUR_OF_A_KIND;
				}
				else if (jokerCount == 3)
				{
					hand.type = HandType.FIVE_OF_A_KIND;
				}
			}
			else
			{
				if (jokerCount == 1)
				{
					hand.type = HandType.ONE_PAIR;
				}
				else if (jokerCount == 2)
				{
					hand.type = HandType.THREE_OF_A_KIND;
				}
				else if (jokerCount == 3)
				{
					hand.type = HandType.FOUR_OF_A_KIND;
				}
				else if (jokerCount == 4)
				{
					hand.type = HandType.FIVE_OF_A_KIND;
				}
				else if (jokerCount == 5)
				{
					hand.type = HandType.FIVE_OF_A_KIND;
				}
			}
		}
	}



	public override double Solve1()
	{
		Dictionary<char, int> cardStrength = new(){
			{'2', 2},
			{'3', 3},
			{'4', 4},
			{'5', 5},
			{'6', 6},
			{'7', 7},
			{'8', 8},
			{'9', 9},
			{'T', 10},
			{'J', 11},
			{'Q', 12},
			{'K', 13},
			{'A', 14},
		};
		double sum = 0;
		List<Hand> hands = [];
		foreach (var input in inputs)
		{
			var split = input.Split(' ');
			string cards = split[0];
			int bid = int.Parse(split[1]);
			hands.Add(new(cards, bid));
		}

		hands.Sort(delegate (Hand hand1, Hand hand2)
		{
			if (hand1.type == hand2.type)
			{
				for (int i = 0; i < 5; i++)
				{
					int diff = cardStrength[hand1.cards[i]] - cardStrength[hand2.cards[i]];
					if (diff != 0)
					{
						return diff;
					}
				}
			}

			return hand1.type - hand2.type;
		});

		int rank = 1;
		foreach (var hand in hands)
		{
			sum += rank * hand.bid;
			rank++;
		}

		return sum;
	}

	public override double Solve2()
	{
		Dictionary<char, int> cardStrength = new(){
			{'J', 1},
			{'2', 2},
			{'3', 3},
			{'4', 4},
			{'5', 5},
			{'6', 6},
			{'7', 7},
			{'8', 8},
			{'9', 9},
			{'T', 10},
			{'Q', 12},
			{'K', 13},
			{'A', 14},
		};
		double sum = 0;
		List<HandWithJoker> hands = [];
		foreach (var input in inputs)
		{
			var split = input.Split(' ');
			string cards = split[0];
			int bid = int.Parse(split[1]);
			hands.Add(new(cards, bid));
		}

		hands.Sort(delegate (HandWithJoker hand1, HandWithJoker hand2)
		{
			if (hand1.hand.type == hand2.hand.type)
			{
				for (int i = 0; i < 5; i++)
				{
					int diff = cardStrength[hand1.cards[i]] - cardStrength[hand2.cards[i]];
					if (diff != 0)
					{
						return diff;
					}
				}
			}

			return hand1.hand.type - hand2.hand.type;
		});

		int rank = 1;
		foreach (var hand in hands)
		{
			sum += rank * hand.hand.bid;
			rank++;
		}

		return sum;
	}

}
