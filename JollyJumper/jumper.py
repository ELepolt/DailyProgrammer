import pdb

challenge_inputs = [[4, 1, 4, 2, 3], [5, 1, 4, 2, -1, 6], [4, 19, 22, 24, 21], [4, 19, 22, 24, 25], [4, 2, -1, 0, 2]]
challenge_outputs = ["JOLLY", "NOT JOLLY", "NOT JOLLY", "JOLLY", "JOLLY"]

def main():
    output = []
    for challenge_input_index, challenge_input in enumerate(challenge_inputs):
        jumps = get_jumps(challenge_input)
        jumper_list = build_jumper_list(jumps)
        remaining_input = get_remaining_input(challenge_input)

        for index in range(0, jumps):
            next_index = index + 1
            if next_index == jumps:
                break

            value = remaining_input[index]
            next_value = remaining_input[next_index]

            difference = abs(value - next_value)
            if difference in jumper_list:
                jumper_list.remove(difference)

        if len(jumper_list) == 0:
            output.append("JOLLY")
        else:
            output.append("NOT JOLLY")

    print(challenge_outputs == output)


def get_jumps(challenge_input):
    """Returns the first number of the input array"""
    return challenge_input[0]

def build_jumper_list(jumps):
    """ Returns the range from 1 to N where N is the number of jumps needed """
    return list(range(1, jumps))

def get_remaining_input(challenge_input):
    """ Returns the challenge input string minus the first number """
    return challenge_input[1:]

if __name__ == "__main__":
    main()
