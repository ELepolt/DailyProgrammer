
challenge_inputs = [[4, 1, 4, 2, 3], [5, 1, 4, 2, -1, 6], [4, 19, 22, 24, 21], [4, 19, 22, 24, 25], [4, 2, -1, 0, 2]]
challenge_outputs = ["JOLLY", "NOT JOLLY", "NOT JOLLY", "JOLLY", "JOLLY"]

def main():
    output = []
    for challenge_input_index, challenge_input in enumerate(challenge_inputs):
        jumps = challenge_input[0]
        jumper_list = set(range(1, jumps))
        remaining_input = challenge_input[1:]

        for index in range(0, jumps):
            if index + 1 == jumps:
                break
            jumper_list.discard(abs(remaining_input[index] - remaining_input[index + 1]))

        output_text = "JOLLY" if len(jumper_list) == 0 else "NOT JOLLY"
        output.append(output_text)

    print(challenge_outputs == output)

if __name__ == "__main__":
    main()
